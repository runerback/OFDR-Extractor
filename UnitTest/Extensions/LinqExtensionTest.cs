using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections;

namespace Extractor.UnitTest.Extensions
{
	[TestFixture]
	public class LinqExtensionTest
	{
		[Test]
		public void LastIndexOf()
		{
			List<int> source = new List<int>()
			{
				0,1,2,3,3,3,4,5,6,7,8,9
			};
			Assert.AreEqual(
				5,
				source.LastIndexOf(item => item == 3));
			Assert.AreEqual(
				-1,
				source.LastIndexOf(item => item == 10));
		}

		[Test]
		public void Count()
		{
			IEnumerable source = new List<int>()
			{
				0,1,2,3,3,3,4,5,6,7,8,9
			};
			Assert.AreEqual(12, source.Count());
		}

		[Test]
		public void Random()
		{
			List<int> source = new List<int>()
			{
				0,1,2,3,3,3,4,5,6,7,8,9
			};
			Assert.DoesNotThrow(delegate
			{
				source.Random();
			});
		}

		[Test]
		public void RandomWithCondition()
		{
			List<int> source = new List<int>()
			{
				0,1,2,3,3,3,4,5,6,7,8,9
			};
			int result = 0;
			Assert.DoesNotThrow(delegate
			{
				result = source.Random(item => item > 3);
			});
			Assert.Less(3, result);
		}
	}
}
