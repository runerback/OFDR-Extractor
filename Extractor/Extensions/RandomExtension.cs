using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
	public static class RandomExtension
	{
		public static T Random<T>(this IEnumerable<T> source)
		{
			int count = source.Count();
			if (count == 0) return default(T);
			else if (count == 1) return source.First();

			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			return source.ElementAt(rnd.Next(count - 1));
		}

		public static T Random<T>(this IEnumerable<T> source, Predicate<T> match)
		{
			if (match == null)
			{
				return source.Random();
			}
			else
			{
				return source.Where(item => match(item)).Random();
			}
		}
	}
}
