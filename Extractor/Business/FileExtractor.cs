using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	public static class FileExtractor
	{
		public static void Extract(this Data.FileData fileData)
		{
			if (fileData == null)
				throw new ArgumentNullException("fileData");

			Extractor extractor = new Extractor(fileData);
			extractor.Extracted += onExtracted;
			extractor.Extract();
		}

		private static void onExtracted(object sender, EventArgs e)
		{
			var extractor = sender as Extractor;
			extractor.Extracted -= onExtracted;
			raiseExtracted(extractor.FileData);
		}

		public static event EventHandler Extracted;

		private static void raiseExtracted(Data.FileData fileData)
		{
			if (Extracted != null)
			{
				Extracted(fileData, EventArgs.Empty);
			}
		}

		private class Extractor
		{
			public Extractor(Data.FileData fileData)
			{
				if (fileData == null)
					throw new ArgumentNullException("fileData");

				this.fileData = fileData;
			}

			private Data.FileData fileData;
			public Data.FileData FileData
			{
				get { return this.fileData; }
			}

			private readonly object locker = new object();

			public void Extract()
			{
				lock (locker)
				{
					var fileData = this.fileData;

					DAT dat = new DAT();
					dat.Exited += onDATExited;

					fileData.TreeNode.State = Data.TreeNodeState.Processing;

					if (fileData.Index == 0)
						dat.Call(fileData.Name);
					else
						dat.Call(fileData.Name, fileData.Index.ToString());
				}
			}

			private void onDATExited(object sender, DAT.ExitedEventArgs e)
			{
				var fileData = this.fileData;

				string unpackingFileName = fileData.OutputFullName;
				string extractedName = Path.Combine(ConfigManager.OutputFolder, fileData.OutputName);
				string targetFullName = Path.Combine(ConfigManager.OutputFolder, unpackingFileName);

				try
				{
					if (File.Exists(extractedName))
					{
						if (e.HasError)
						{
							File.Delete(extractedName);
							throw new Exception(e.Error);
						}
						else
						{
							if (!Directory.Exists(targetFullName))
							{
								Directory.CreateDirectory(targetFullName);
							}
							File.Move(extractedName, targetFullName);
							fileData.TreeNode.State = Data.TreeNodeState.Ready;

							raiseExtracted();
						}
					}
					else
					{
						throw new Exception(
							string.Format("Cannot extract file \"{0}\"", unpackingFileName));
					}
				}
				catch
				{
					fileData.TreeNode.State = Data.TreeNodeState.Error;
					throw;
				}
			}

			public event EventHandler Extracted;

			private void raiseExtracted()
			{
				if (Extracted != null)
				{
					Extracted(this, EventArgs.Empty);
				}
			}
		}
	}
}
