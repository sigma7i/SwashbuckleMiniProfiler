using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Extensions;
using Swashbuckle.Application;
using Swashbuckle.Swagger;

[assembly: PreApplicationStartMethod(typeof(WebApiSwagger.Swagger.SwaggerConfig), "RegisterGlobal")]

namespace WebApiSwagger.Swagger
{
	/// <summary>
	///  Конфигурация Swagger
	/// </summary>
	public static class SwaggerConfig
	{
		/// <summary>
		/// Регистрация Swagger используя GlobalConfiguration.Configuration
		/// </summary>
		/// <remarks>
		///  Для поддержики запуска через [assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "RegisterGlobal")]
		/// </remarks>
		public static void RegisterGlobal()
		{
			Register(GlobalConfiguration.Configuration);
		}

		/// <summary>
		/// Регистрация Swagger
		/// </summary>
		public static void Register(HttpConfiguration httpConfiguration)
		{
			bool enableSwagger = AppSettings.Get<bool>("EnableSwagger");

			if (enableSwagger)
				httpConfiguration
					.EnableSwagger(ConfigureSwagger)
					.EnableSwaggerUi(ConfigureSwaggerUi);
		}

		/// <summary>
		/// Расширение для автоматического генеририрования enum
		/// </summary>
		private sealed class ApplyDocumentEnumExtension : IDocumentFilter
		{
			public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
			{
				foreach (var schemaDictionaryItem in swaggerDoc.definitions)
				{
					var schema = schemaDictionaryItem.Value;
					foreach (var propertyDictionaryItem in schema.properties)
					{
						var property = propertyDictionaryItem.Value;
						var propertyEnums = property.@enum;
						if (propertyEnums != null && propertyEnums.Count > 0)
						{
							var enumDescriptions = new List<string>();
							for (int i = 0; i < propertyEnums.Count; i++)
							{
								var enumOption = propertyEnums[i];
								enumDescriptions.Add(string.Format("{0} = {1} ", Convert.ToInt32(enumOption), Enum.GetName(enumOption.GetType(), enumOption)));
							}
							property.description += string.Format(" ({0})", string.Join(", ", enumDescriptions.ToArray()));
						}
					}
				}
			}
		}

		/// <summary>
		/// Позволяет добавлять на форму, удобный механизм загрузки файла
		/// </summary>
		private sealed class ApplyImportFileOperationsExtension : IOperationFilter
		{
			public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
			{
				var requestAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerUploadAttribute>();
				foreach (var attr in requestAttributes)
				{
					if (operation.parameters == null)
						operation.parameters = new List<Parameter>();

					operation.parameters.Add(
					new Parameter
					{
						description = attr.Description,
						name = attr.ParameterName,

						@in = "formData",
						required = true,
						type = "file",
					});

					operation.consumes.Add("multipart/form-data");
				}
			}
		}

		/// <summary>
		/// Конфигурирование Swagger
		/// </summary>
		public static void ConfigureSwagger(SwaggerDocsConfig config)
		{
			config.SingleApiVersion("v1", "WebApiSwagger");
			config.RootUrl(req => new Uri(req.RequestUri, HttpContext.Current.Request.ApplicationPath ?? string.Empty).ToString());

			SetXmlCommentsPathForControllers(config);
			SetXmlCommentsPathForModels(config);

			config.GroupActionsBy(apiDescription => apiDescription.ActionDescriptor.ControllerDescriptor.ControllerName);
			config.OrderActionGroupsBy(Comparer<string>.Default);
			config.DocumentFilter<ApplyDocumentEnumExtension>();
			config.DocumentFilter<InjectMiniProfiler>();
			config.OperationFilter<ApplyImportFileOperationsExtension>();
			config.PrettyPrint();
		}

		/// <summary>
		/// Конфигурирование Swagger UI.
		/// </summary>
		public static void ConfigureSwaggerUi(SwaggerUiConfig config)
		{
			config.InjectJavaScript(typeof(SwaggerConfig).Assembly, "WebApiSwagger.Swagger.SwaggerUiCustomization.js");
			config.InjectJavaScript(typeof(SwaggerConfig).Assembly, "WebApiSwagger.Swagger.SwaggerDateInput.js");
		}

		/// <summary>
		/// Получает XML-комментарий для контроллеров.
		/// </summary>
		private static void SetXmlCommentsPathForControllers(SwaggerDocsConfig config)
		{
			string controllersPath;

			if (TryGetXmlCommentsPath(out controllersPath))
				config.IncludeXmlComments(controllersPath);
		}

		/// <summary>
		/// Получает XML-комментарий для моделей.
		/// </summary>
		private static void SetXmlCommentsPathForModels(SwaggerDocsConfig config)
		{
			string modelsPath;

			if (TryGetXmlCommentsPath(out modelsPath))
				config.IncludeXmlComments(modelsPath);
		}

		/// <summary>
		/// Получает xml путь комментариев из сборки для указанного типа
		/// </summary>
		/// <param name="xmlPath">путь к файлу XML комментарию</param>
		private static bool TryGetXmlCommentsPath(out string xmlPath)
		{
			var projectName = Assembly.GetExecutingAssembly().GetName().Name;

			xmlPath = string.Format(@"{0}\bin\{1}.XML", AppDomain.CurrentDomain.BaseDirectory, projectName);

			var fileExists = File.Exists(xmlPath);

			return fileExists;
		}
	}
}