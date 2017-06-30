using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Extractor.UnitTest.Business
{
	[TestFixture]
	public class FileUnpackingManagerTest
	{
		private Extractor.Data.FolderData rootFolder;

		public FileUnpackingManagerTest()
		{
			this.rootFolder = LZSSFileMapParserTest.RootFolder;
			Extractor.Business.FileUnpackingManager.Completed += this.onFieUnpackingComplted;
			this.blocker = new AutoResetEvent(true);
		}

		private AutoResetEvent blocker;

		[Test]
		public void UnpackSingleFile()
		{
			try
			{
				var targetFolder = this.rootFolder.SubFolders.Random(folder => folder.Files.Count > 0);
				Assert.NotNull(targetFolder, "target folder");
				var targetFile = targetFolder.Files.Random();
				Assert.NotNull(targetFile, "target file");
				this.blocker.Reset();
				Extractor.Business.FileUnpackingManager.Unpack(targetFile);
				this.blocker.WaitOne();

				Assert.AreEqual(Extractor.Data.TreeNodeState.Ready, targetFile.TreeNode.State);
			}
			finally
			{
				this.releaseResources();
			}
		}

		[Test]
		public void UnpackSingleFolder()
		{
			try
			{
				var rootFolder = this.rootFolder;
				var targetFolder = rootFolder.SubFolders.Random(folder => folder.Files.Count > 0);
				Assert.NotNull(targetFolder, "target folder");
				foreach (var targetFile in targetFolder.Files)
				{
					this.blocker.Reset();
					Extractor.Business.FileUnpackingManager.Unpack(targetFile);
					this.blocker.WaitOne();

					Assert.AreEqual(Data.TreeNodeState.Ready, targetFile.TreeNode.State);
				}
			}
			finally
			{
				this.releaseResources();
			}
		}

		[Test]
		public void UnpackNestedFolder()
		{
			try
			{
				var rootFolder = this.rootFolder;
				var targetFolder = rootFolder.SubFolders.Random(folder => folder.Files.Count > 0);

				Extractor.Data.FolderData fakeFolder = new Extractor.Data.FolderData("test");
				fakeFolder.Add(new Extractor.Data.FileData(new Extractor.Data.LZSSFileItem()
				{
					Name = "test.txt",
					Index = 0,
					Size = 100
				}));
				fakeFolder.Add(new Extractor.Data.FileData(new Extractor.Data.LZSSFileItem()
				{
					Name = "test.txt",
					Index = 1,
					Size = 100
				}));
				//root.SubFolders[0].Add(fakeFolder);
			}
			finally
			{
				this.releaseResources();
			}
		}

		private void onFieUnpackingComplted(object sender, EventArgs e)
		{
			Console.WriteLine("file unpacking completed: {0}", (sender as Extractor.Data.FileData).Name);
			this.blocker.Set();
		}

		private void releaseResources()
		{
			Extractor.Business.FileUnpackingManager.Completed -= this.onFieUnpackingComplted;
			this.blocker.Close();
			this.blocker.Dispose();
			this.blocker = null;
		}

	}
}
