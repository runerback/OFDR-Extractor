using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTest.Models
{
	[TestFixture]
	public class FileDataTest
	{
		[Test]
		public void FormatSize()
		{
			var lzssFileInfo = new Extractor.Models.LZSS.FileItem();

			lzssFileInfo.Size = 999;
			var fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("999 B", fileData.FormattedSize);

			lzssFileInfo.Size = 1023; 
			fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("1 K", fileData.FormattedSize);

			lzssFileInfo.Size = 1025;
			fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("1 K", fileData.FormattedSize);

			lzssFileInfo.Size = 541696;
			fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("529 K", fileData.FormattedSize);

			lzssFileInfo.Size = 1099511627776;
			fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("1 T", fileData.FormattedSize);
		}

		[Test]
		public void OutputName()
		{
			var lzssFileInfo = new Extractor.Models.LZSS.FileItem()
			{
				Name = "test file.txt",
				Size = 1
			};

			lzssFileInfo.Index = 0;
			var fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("test file.txt", fileData.OutputName);

			lzssFileInfo.Index = 1;
			fileData = new Extractor.Models.FileData(lzssFileInfo);
			Assert.AreEqual("test file.txt1", fileData.OutputName);
		}
	}
}
