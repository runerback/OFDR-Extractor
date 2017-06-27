using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest.Business
{
	[TestFixture]
	public class IOFilesManagerTest
	{
		private string outputFileName = "file map.xml";

		[Test]
		public void Export()
		{
			var parser = new LZSSFileMapParserTest();
			parser.Parse();
			Assert.DoesNotThrow(() => Extractor.Business.IOFilesManager.Export(parser.RootFolder, this.outputFileName));
		}

		[Test]
		public void Import()
		{
			var root = Extractor.Business.IOFilesManager.Import(this.outputFileName);
			Assert.NotNull(root);
		}
	}
}
