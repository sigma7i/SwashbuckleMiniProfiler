using System.Web.Http;
using StackExchange.Profiling;

namespace WebApiSwagger.Controllers
{
	[RoutePrefix("api/v1/default")]
	public class DefaultController : ApiController
	{
		[Route("")]
		public IHttpActionResult Get()
		{
			return Json(new
			{
				Name = "Marco",
				Description = "I need some profiling!`"
			});
		}

		[Route("step")]
		public IHttpActionResult GetWithStep()
		{
			var profiler = MiniProfiler.Current;

			using (profiler.Step("Starting a profiling Step"))
			{
				return Json(new
				{
					Name = "Marco",
					Description = "I haz profiling!`"
				});
			}
		}

		[Route("notfound")]
		public IHttpActionResult NothingToSeeHere()
		{
			return NotFound();
		}

		[HttpPost]
		[Route("posting")]
		public IHttpActionResult PostSomething(string stuff)
		{
			return Ok();
		}

		[Route("broken")]
		public IHttpActionResult ThrowAnError()
		{
			throw new System.Exception("Error error");
		}
	}
}