﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using WebApiSwagger.Swagger;

namespace WebApiSwagger
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			InjectMiniProfiler.ConfigureMiniProfiler();
		}

		protected void Application_BeginRequest()
		{
			//if (Request.IsLocal)
			MiniProfiler.Start();
		}

		protected void Application_EndRequest()
		{
			//if (Request.IsLocal)
			MiniProfiler.Stop();
		}
	}
}
