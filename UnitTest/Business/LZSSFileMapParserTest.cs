using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace UnitTest.Business
{
	[TestFixture]
	public class LZSSFileMapParserTest
	{
		public LZSSFileMapParserTest()
		{
			Extractor.Business.DATManager.DataReceived += this.onDataReceived;
			Extractor.Business.DATManager.Exited += this.onExited;
			Console.Write("callDAT start");
			this.callDAT();
			Console.Write("callDAT end");
			Extractor.Business.DATManager.DataReceived -= this.onDataReceived;
			Extractor.Business.DATManager.Exited -= this.onExited;
		}

		private AutoResetEvent blocker;

		private void callDAT()
		{
			this.blocker = new AutoResetEvent(false);
			try
			{
				Assert.Greater(Extractor.Business.DATManager.Call(), -1);
			}
			finally
			{
				this.blocker.WaitOne();
				this.blocker.Close();
				this.blocker.Dispose();
				this.blocker = null;
			}
		}

        private void onDataReceived(int id, string data)
        {
            this.lzssFileMap = data;
        }

		private void onExited(int id)
		{
			this.blocker.Set();
		}
        
		private string lzssFileMap;

		[Test]
		public void Parse()
		{
			Extractor.Models.FolderData rootFolder = null;
			Assert.DoesNotThrow(delegate
			{
				rootFolder = Extractor.Business.LZSSFileMapParser.Parse(this.lzssFileMap);
			});
			Assert.NotNull(rootFolder);
			LZSSFileMapParserTest.rootFolder = rootFolder;
		}

		private static Extractor.Models.FolderData rootFolder;
		public static Extractor.Models.FolderData RootFolder
		{
			get { return LZSSFileMapParserTest.rootFolder; }
		}
	}
}
