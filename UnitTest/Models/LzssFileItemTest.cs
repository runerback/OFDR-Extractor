using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Extractor.UnitTest.Models
{
	[TestFixture]
	public class LzssFileItemTest
	{
		[Test]
		public void Parse()
		{
			string datOutput = @"\r\ntest file.xml 1\r\ntest file.xml 2\r\n";
			var list = Data.LZSSFileItem.Parse(datOutput);

			Assert.NotNull(list);
			Assert.AreEqual(2, list.Count);

			var item1 = list[0];
			Assert.True(
				item1.Name == "test file.xml" &&
				item1.Size == 1 &&
				item1.Index == 0);

			var item2 = list[1];
			Assert.True(
				item2.Name == "test file.xml" &&
				item2.Size == 2 &&
				item2.Index == 1);
		}
	}
}
