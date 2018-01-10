using System.Linq;

namespace Common.Extensions
{
	public static class IQueryableExtensions
	{
		/// <summary>
		///  Реализация постраничного вывода коллекции
		/// </summary>
		/// <param name="page">Страница</param>
		/// <param name="count">Количество строк в одной странице</param>
		public static IQueryable<T> ToPagedQuery<T>(this IQueryable<T> query, int page, int count)
		{
			if (page <= 0) page = 1;
			if (count <= 0) count = 1;

			return query.Skip(count * (page - 1)).Take(count);
		}
	}
}
