using System.Linq;
using System.Web.Http.Description;
using StackExchange.Profiling;
using Swashbuckle.Swagger;

namespace WebApiSwagger.Swagger
{
	public class InjectMiniProfiler : IDocumentFilter
	{
		public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
		{
			swaggerDoc.info.contact = new Contact()
			{
				name = MiniProfiler.RenderIncludes().ToHtmlString()
			};
		}

		/// <summary>
		///  Конфигурирование MiniProfiler
		/// </summary>
		public static void ConfigureMiniProfiler()
		{
			IgnoredPaths();
		}

		/// <summary>
		/// Игнорирование путей для MiniProfiler
		/// </summary>
		private static void IgnoredPaths()
		{
			var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();
			ignored.Add("swagger/ui/images/");
			ignored.Add("swagger/ui/css/");
			//ignored.Add("swagger/ui/docs/");
			ignored.Add("swagger/ui/index");
			ignored.Add("swagger/ui/lib/");
			ignored.Add("swagger/ui/swagger-ui-min-js");
			ignored.Add("swagger/ui/ext/Retail-Biometry-WebApi");
			MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();
		}
	}
}