using System.Collections.Generic;

namespace System.Linq
{
	public static class IndexOfExtention
	{
		public static int LastIndexOf<T>(this IEnumerable<T> source, Predicate<T> match)
		{
			if (source == null) return -1;

			int index = source.Count() - 1;

			if (index < 0) return -1;

			foreach (var item in source.Reverse())
			{
				if (match(item))
					break;
				index--;
			}
			return index;
		}
	}
}
