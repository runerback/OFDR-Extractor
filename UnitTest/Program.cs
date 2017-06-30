using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.UnitTest
{
	[SetUpFixture]
	public class Program
	{
		static Program()
		{
			Assert.True(Extractor.Business.ConfigManager.CheckConfigurations());
			Assert.DoesNotThrow(delegate
			{
				new Business.LZSSFileMapParserTest().Parse();
			});
		}
	}
}
