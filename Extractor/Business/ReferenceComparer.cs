using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	public static class ReferenceComparer
	{
		public static object Left { get; set; }
		public static object Right { get; set; }

		public static bool IsReferenceEqual()
		{
			if (Left == null || Right == null)
			{
				return false;
			}
			return object.ReferenceEquals(Left, Right);
		}
	}
}
