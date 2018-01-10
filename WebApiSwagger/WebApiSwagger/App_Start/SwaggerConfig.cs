//using System.Web.Http;
//using Swashbuckle.Application;
//using WebActivatorEx;
//using WebApiSwagger;
//using WebApiSwagger.Swagger;

//[assembly: PreApplicationStartMethod(typeof(WebApiSwagger.SwaggerConfig), "Register")]

//namespace WebApiSwagger
//{
//	public class SwaggerConfig
//	{
//		public static void Register()
//		{
//			var thisAssembly = typeof(SwaggerConfig).Assembly;

//			GlobalConfiguration.Configuration
//				.EnableSwagger(c =>
//				{
//					c.SingleApiVersion("v1", "WebApiSwagger");
//					c.DocumentFilter<InjectMiniProfiler>();
//				})
//				.EnableSwaggerUi(c =>
//				{
//					c.InjectJavaScript(thisAssembly, "WebApiSwagger.Swagger.SwaggerUiCustomization.js");
//				});
//		}
//	}
//}
