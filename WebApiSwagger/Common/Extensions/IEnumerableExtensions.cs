using System;
using System.Collections.Generic;

namespace Common.Extensions
{
	public static class IEnumerableExtensions
	{
		public static void BreakableForEach<T>(this IEnumerable<T> enumeration, Func<T, bool> iteration)
		{
			if (enumeration == null)
				throw new ArgumentNullException(nameof(enumeration));
			if (iteration == null)
				throw new ArgumentNullException(nameof(iteration));

			foreach (var item in enumeration)
			{
				if (iteration(item))
					break;
			}
		}

		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> iteration)
		{
			if (iteration == null)
				throw new ArgumentNullException(nameof(iteration));

			BreakableForEach(
				enumeration,
				item =>
				{
					iteration(item);

					return false;
				}
			);
		}


		private static IEnumerable<TSource> FromHierarchy<TSource>(
			 TSource source,
			 Func<TSource, TSource> nextItem,
			 Func<TSource, bool> canContinue)
		{
			for (var current = source; canContinue(current); current = nextItem(current))
			{
				yield return current;
			}
		}

		/// <summary>
		///  Выражение для запросов по дереву, пока nextItem != null
		/// </summary>
		/// <example>InnerException.FromHierarchy(ex => ex.InnerException)</example>
		public static IEnumerable<TSource> FromHierarchy<TSource>(
			this TSource source,
			Func<TSource, TSource> nextItem)
			where TSource : class
		{

			return FromHierarchy(source, nextItem, s => s != null);
		}
	}
}
