using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Models
{
	public class FolderData : FileDataBase
	{
		public FolderData(string name)
		{
			this.files = new List<FileData>();
			this.subFolders = new List<FolderData>();
			this.name = name;
			this.treeNode = new TreeNodeModel(this);
		}

		private List<FileData> files;
		public List<FileData> Files
		{
			get { return this.files; }
		}

		private List<FolderData> subFolders;
		public List<FolderData> SubFolders
		{
			get { return this.subFolders; }
		}

		private int level = 0;
		public int Level
		{
			get { return this.level; }
		}

		private Common.AutoInvokeObservableCollection<FileDataBase> source =
			new Common.AutoInvokeObservableCollection<FileDataBase>();
		public Common.AutoInvokeObservableCollection<FileDataBase> Source
		{
			get { return this.source; }
		}

		public void Add(FolderData subFolder)
		{
			if (subFolder != null)
			{
				this.subFolders.Add(subFolder);
				subFolder.ParentFolder = this;
				subFolder.level = this.level + 1;
				this.source.Add(subFolder);
			}
		}

		public void Remove(FolderData subFolder)
		{
			if (subFolder != null && this.subFolders.Contains(subFolder))
			{
				if (this.subFolders.Remove(subFolder))
				{
					subFolder.ParentFolder = null;
					subFolder.level = -1;
					this.source.Remove(subFolder);
				}
			}
		}

		public void Add(FileData file)
		{
			if (file != null)
			{
				this.files.Add(file);
				file.ParentFolder = this;
				this.source.Add(file);
			}
		}

		public void RemoveFile(FileData file)
		{
			if (file != null && this.files.Contains(file))
			{
				if (this.files.Remove(file))
				{
					file.ParentFolder = null;
					this.source.Remove(file);
				}
			}
		}
	}
}
