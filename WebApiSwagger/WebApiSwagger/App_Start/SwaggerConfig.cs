using System.Web.Http;
using Swashbuckle.Application;
using WebActivatorEx;
using WebApiSwagger;


[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApiSwagger
{
	public class SwaggerConfig
	{
		public static void Register()
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;

			GlobalConfiguration.Configuration
				.EnableSwagger(c =>
				{
					c.SingleApiVersion("v1", "WebApiSwagger");
					c.DocumentFilter<App_Start.InjectMiniProfiler>();
				})
				.EnableSwaggerUi(c =>
				{
					c.InjectJavaScript(thisAssembly, "WebApiSwagger.App_Start.SwaggerUiCustomization.js");
				});
		}
	}
}
