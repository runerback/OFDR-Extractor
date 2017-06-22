using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
	public static class ForeachExtension
	{
		public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (action != null)
			{
				foreach (var item in source)
				{
					action(item);
				}
			}
		}

		public static void Foreach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			if (action != null)
			{
				int index = 0;
				foreach (var item in source)
				{
					action(item, checked(index++));
				}
			}
		}
	}
}
