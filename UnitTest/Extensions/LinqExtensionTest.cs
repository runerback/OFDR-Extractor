using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTest.Extensions
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
	}
}
