using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor.Business
{
	public static class FileUnpackingManager
	{
		#region monitor

		private static FileSystemWatcher watcher;

		public static void CreateMonitor()
		{
			if (watcher == null)
			{
				watcher = new FileSystemWatcher(Business.ConfigManager.OFDRRoot);
				watcher.IncludeSubdirectories = true;
				watcher.NotifyFilter = NotifyFilters.FileName;
				watcher.Created += onFileSystemStateChanged;
				watcher.Changed += onFileSystemStateChanged;
				watcher.Deleted += onFileSystemStateChanged;
				watcher.Renamed += onFileSystemStateChanged;
			}
		}

		public static void StartMonitor()
		{
			if (watcher != null)
				watcher.EnableRaisingEvents = true;
		}

		public static void StopMonitor()
		{
			if (watcher != null)
				watcher.EnableRaisingEvents = false;
		}

		public static void DestroyMonitor()
		{
			if (watcher != null)
			{
				watcher.EnableRaisingEvents = false;
				watcher.Created -= onFileSystemStateChanged;
				watcher.Changed -= onFileSystemStateChanged;
				watcher.Deleted -= onFileSystemStateChanged;
				watcher.Renamed -= onFileSystemStateChanged;
				watcher.Dispose();
				watcher = null;
			}
		}

		private static void onFileSystemStateChanged(object sender, FileSystemEventArgs e)
		{

		}

		#endregion monitor

		static FileUnpackingManager()
		{
			//check file after DAT.exe exited
			Business.DATManager.Exited += onDATExited;
		}

		private static ConcurrentDictionary<int, Models.FileData> processMap = new ConcurrentDictionary<int, Models.FileData>();

		private static void unpack(Models.FileData file)
		{
			file.TreeNode.State = Models.TreeNodeState.Processing;
			int processID = Business.DATManager.Call(file.Name, file.Index.ToString());
			processMap.TryAdd(processID, file);
		}

		private static void onDATExited(int processID)
		{
			Models.FileData file;
			if (processMap.TryRemove(processID, out file))
			{
				onFileUnpacked(file);
			}
		}

		private static void onFileUnpacked(Models.FileData file)
		{
			FileInfo unpackedFile = new FileInfo(file.OutputName);
			if (!unpackedFile.Exists)
			{
				file.TreeNode.State = Models.TreeNodeState.Error;
				return;
			}

			unpackedFile.CopyTo(file.OutputFullName);
			file.TreeNode.State = Models.TreeNodeState.Ready;
		}
	}
}
