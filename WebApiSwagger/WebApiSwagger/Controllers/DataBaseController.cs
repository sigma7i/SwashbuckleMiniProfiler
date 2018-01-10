using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Swashbuckle.Swagger.Annotations;

namespace WebApiSwagger.Controllers
{
	public class DataBaseController : ApiController
	{
		/// <summary>
		/// Получение постраничного списка пользователей
		/// </summary>
		/// <param name="page">Страница</param>
		/// <param name="count">Количество строк в одной странице</param>
		[HttpGet, Route("GetUserList")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<User>))]
		public IHttpActionResult GetList(int page = 1, int count = 50)
		{
			var repo = new UsersRepository(new BiometryDbConnectionFactory());
			var result = repo.Get(page, count);
			return Ok(result);
		}

		/// <summary>
		/// Получение списка ролей
		/// </summary>
		[HttpGet, Route("GetRoleList")]
		[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Role))]
		public IHttpActionResult GetList()
		{
			var repo = new RolesRepository(new BiometryDbConnectionFactory());
			var result = repo.Get();
			return Ok(result);
		}
	}
}
