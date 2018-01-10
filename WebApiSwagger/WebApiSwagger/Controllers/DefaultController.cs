using System.Web.Http;
using DataAccess;
using DataAccess.Repositories;
using StackExchange.Profiling;

namespace WebApiSwagger.Controllers
{
	/// <summary>
	///  Описание контроллера
	/// </summary>
	[RoutePrefix("api/v1/default")]
	public class DefaultController : ApiController
	{
		/// <summary>
		/// Описание
		/// </summary>
		/// <returns></returns>
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

		[HttpGet, Route("GetUserList")]
		public IHttpActionResult GetList(int page = 1, int count = 50)
		{
			var repo = new UsersRepository(new BiometryDbConnectionFactory());
			var result = repo.Get(page, count);
			return Ok(result);
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

		/// <summary>
		/// Throws Exception
		/// </summary>
		[Route("broken")]
		public IHttpActionResult ThrowAnError()
		{
			throw new System.Exception("Error error");
		}
	}
}