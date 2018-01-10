using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Common.Extensions;

namespace WebApiSwagger.ExceptionHandling
{
	/// <summary>
	/// Глобальный обработчик ошибок
	/// </summary>
	public class BiometryExceptionHandler : ExceptionHandler
	{
		private const string StandartDetails = "Произошла ошибка.";

		public override void Handle(ExceptionHandlerContext context)
		{
			context.Result = CreateMessage(context);
		}

		public override bool ShouldHandle(ExceptionHandlerContext context)
		{
			return true;
		}



		/// <summary>
		/// Отображение всех ошибок, любой вложенности
		/// </summary>
		private IHttpActionResult CreateMessage(ExceptionHandlerContext context)
		{
			// просматриваем ошибки любой вложенности
			var exeptions = context.Exception.FromHierarchy(ex => ex.InnerException);
			var messages = exeptions.Select(ex => ex.Message);

			string stackTrace;

			if (!_isDebug)
				stackTrace = StandartDetails;
			else
				stackTrace = context.Exception.ToString();

			return CreateMessage(context, string.Join(" -> ", messages), stackTrace);
		}

		private bool _isDebug => AppSettings.Get<bool>("IsDebug");

		private IHttpActionResult CreateMessage(ExceptionHandlerContext context, string message, string details)
		{
			var info = new ClientExceptionInformation { Message = message, DetailedInformation = details };

			return new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, info));
		}
	}
}