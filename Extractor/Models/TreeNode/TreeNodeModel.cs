using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public class TreeNodeModel : Common.ViewModelBase
	{
		public TreeNodeModel(FileDataBase fileData)
		{
			if (fileData == null)
			{
				throw new ArgumentNullException("fileData in TreeNodeModel .ctor");
			}

			if (fileData is FileData)
			{
				this.nodeType = TreeNodeType.File;
			}
			else if (fileData is FolderData)
			{
				this.nodeType = TreeNodeType.Folder;
			}
			else
			{
				throw new ArgumentException("does not support this type of fileData");
			}

			this.fileData = fileData;
		}

		protected bool? isSelected = false;
		public bool? IsSelected
		{
			get { return this.isSelected; }
			set
			{
				if (this.isSelected != value)
				{
					this.isSelected = value;
					this.NotifyPropertyChanged("IsSelected");
				}
			}
		}

		private TreeNodeType nodeType;
		public TreeNodeType NodeType
		{
			get { return this.nodeType; }
		}

		public string NodeTypeInString
		{
			get { return this.nodeType.ToString(); }
		}

		protected TreeNodeState state = TreeNodeState.StandBy;
		public TreeNodeState State
		{
			get { return this.state; }
			set
			{
				if (this.state != value)
				{
					this.state = value;
					this.NotifyPropertyChanged("State");
				}
			}
		}

		private FileDataBase fileData;
		public FileDataBase FileData
		{
			get { return this.fileData; }
		}
	}
}
