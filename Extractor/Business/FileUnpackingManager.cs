using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extractor.Business
{
	public static class FileUnpackingManager
	{
		static FileUnpackingManager()
		{
			//check output file after DAT.exe exited
			Business.DATManagerSingleton.Exited += onDATExited;
		}

		public static void Unpack(Data.FileDataBase fileData)
		{
			if (fileData != null)
			{
				if (fileData.TreeNode.NodeType == Data.TreeNodeType.File)
				{
					Task.Factory.StartNew(unpackFile, fileData);
				}
				else if (fileData.TreeNode.NodeType == Data.TreeNodeType.Folder)
				{
					Task.Factory.StartNew(unpackFile, fileData);
				}
				else
				{
					throw new NotImplementedException(string.Format("TreeNodeType.{0}", fileData.TreeNode.NodeType));
				}
			}
		}

		private static ConcurrentDictionary<int, Data.FileData> processMap = new ConcurrentDictionary<int, Data.FileData>();
		
		private static void unpackFile(object obj)
		{
			var file = obj as Data.FileData;
			file.TreeNode.State = Data.TreeNodeState.Processing;
			int processID = Business.DATManagerSingleton.Call(file.Name, file.Index.ToString());
			processMap.TryAdd(processID, file);
		}

		private static void unpackFolder(object obj)
		{
			var folder = obj as Data.FolderData;
			var files = folder.Files
				.Where(file => file.IsChecked)
				.ToList();
			
		}

		private static void onDATExited(int processID)
		{
			Data.FileData file;
			if (processMap.TryRemove(processID, out file))
			{
				moveToTargetFolder(file);
			}
			else
			{
				file.TreeNode.State = Data.TreeNodeState.Error;
			}
			raiseCompleted(file);
		}

		private static void moveToTargetFolder(Data.FileData file)
		{
			try
			{
				FileInfo unpackedFile = new FileInfo(Path.Combine(ConfigManager.OFDRRootFolder, file.OutputName));
				if (!unpackedFile.Exists)
				{
					Console.WriteLine("cannot unpack file: \"{0}\"", file.Name);
					file.TreeNode.State = Data.TreeNodeState.Error;
					return;
				}
				Console.WriteLine("file unpacked: \"{0}\"", file.Name);

				FileInfo targetFile = new FileInfo(Path.Combine(ConfigManager.OutputFolder, file.OutputFullName));
				if (!targetFile.Directory.Exists)
				{
					targetFile.Directory.Create();
				}
				unpackedFile.MoveTo(targetFile.FullName);
				Console.WriteLine("file moved to: \"{0}\"", targetFile.FullName);

				file.TreeNode.State = Data.TreeNodeState.Ready;
			}
			catch
			{
				file.TreeNode.State = Data.TreeNodeState.Error;
				throw;
			}
		}

		public static event EventHandler Completed;
		private static void raiseCompleted(Data.FileData file)
		{
			if (Completed != null)
			{
				Completed(file, EventArgs.Empty);
			}
		}
	}
}
