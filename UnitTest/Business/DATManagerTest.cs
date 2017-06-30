using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace Extractor.UnitTest.Business
{
	[TestFixture]
	public class DATManagerTest
	{
		public DATManagerTest()
		{
			Extractor.Business.DATManager.DataReceived += this.onDataReceived;
			Extractor.Business.DATManager.Exited += this.onExited;
            this.blocker = new AutoResetEvent(true);
		}

        private AutoResetEvent blocker;

        [Test]
		public void CallWithoutParam()
		{
			try
			{
				this.blocker.Reset();
				Assert.Greater(Extractor.Business.DATManager.Call(), -1);
				this.blocker.WaitOne();
			}
			finally
			{
				this.releaseResources();
			}
		}

		[Test]
		public void CallWithFirstFileInfo()
        {
			try
			{
				this.blocker.Reset();
				Assert.Greater(Extractor.Business.DATManager.Call("logo_sting_out.ambx_bn"), -1);
				this.blocker.WaitOne();
			}
			finally
			{
				this.releaseResources();
			}
        }

		[Test]
		public void CallWithDeeperFileInfo()
        {
			try
			{
				this.blocker.Reset();
				Assert.Greater(Extractor.Business.DATManager.Call("level.lub", "5"), -1);
				this.blocker.WaitOne();
			}
			finally
			{
				this.releaseResources();
			}
        }

        [Test]
        public void CallWithWrongFileInfo()
        {
			try
			{
				this.blocker.Reset();
				Assert.Greater(Extractor.Business.DATManager.Call("file_does_not_exist.hora", "1"), -1);
				this.blocker.WaitOne();
			}
			finally
			{
				this.releaseResources();
			}
        }

		private void onDataReceived(int id, string data)
		{
			Console.WriteLine("working process data received with ID {0}: \r\n{1}", id, data);
		}

        private void onExited(int id)
        {
            Console.WriteLine("working process exited with ID {0}", id);
            this.blocker.Set();
        }

		private void releaseResources()
		{
			Extractor.Business.DATManager.DataReceived -= this.onDataReceived;
			Extractor.Business.DATManager.Exited -= this.onExited;
            this.blocker.Close();
            this.blocker.Dispose();
            this.blocker = null;
        }
	}
}
