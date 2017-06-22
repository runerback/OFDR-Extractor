using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
	public static class CountExtension
	{
		public static int Count(this IEnumerable source)
		{
			var enumerator = source.GetEnumerator();
			int count = 0;
			while (enumerator.MoveNext())
			{
				count++;
			}
			return count;
		}
	}
}
