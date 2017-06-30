using System.Linq;
using System.Collections.Generic;

namespace Extractor.Data
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

		private List<FolderData> subFolders;
		public List<FolderData> SubFolders
		{
			get { return this.subFolders; }
		}

		private List<FileData> files;
		public List<FileData> Files
		{
			get { return this.files; }
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

				int index = this.source.LastIndexOf(
					item => item.TreeNode.NodeType == TreeNodeType.Folder);
				if (index < 0)
					this.source.Insert(0, subFolder);
				else if (index < this.source.Count)
					this.source.Add(subFolder);
				else
					this.source.Insert(index - 1, subFolder);

				this.NotifyPropertyChanged("Source");
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
					this.NotifyPropertyChanged("Source");
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
				this.NotifyPropertyChanged("Source");
			}
		}

		public void Remove(FileData file)
		{
			if (file != null && this.files.Contains(file))
			{
				if (this.files.Remove(file))
				{
					file.ParentFolder = null;
					this.source.Remove(file);
					this.NotifyPropertyChanged("Source");
				}
			}
		}

		public void Add(FileDataBase fileData)
		{
			if (fileData != null)
			{
				if (fileData.TreeNode.NodeType == TreeNodeType.File)
				{
					this.Add(fileData as FileData);
				}
				else if (fileData.TreeNode.NodeType == TreeNodeType.Folder)
				{
					this.Add(fileData as FolderData);
				}
			}
		}

		public void Remove(FileDataBase fileData)
		{
			if (fileData != null)
			{
				if (fileData.TreeNode.NodeType == TreeNodeType.File)
				{
					this.Remove(fileData as FileData);
				}
				else if (fileData.TreeNode.NodeType == TreeNodeType.Folder)
				{
					this.Remove(fileData as FolderData);
				}
			}
		}

		public override bool CanMoveTo(FileDataBase destination)
		{
			if (destination.TreeNode.NodeType == TreeNodeType.File)
			{
				if (destination.ParentFolder == this)
					return false;
			}
			return base.CanMoveTo(destination);
		}
	}
}
