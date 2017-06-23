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
					if (this.controlIsSelectedStateFromBackend)
					{
						this.isSelected = value;
						this.NotifyPropertyChanged("IsSelected");
					}
					else
					{
						if (!value.HasValue)
						{
							this.setIsSelected(false);
						}
						else
						{
							this.isSelected = value;
						}
					}
					this.onSelectionStateChanged(value);
				}
			}
		}

		private bool controlIsSelectedStateFromBackend = false;
		private void setIsSelected(bool? value)
		{
			this.controlIsSelectedStateFromBackend = true;
			this.IsSelected = value;
			this.controlIsSelectedStateFromBackend = false;
		}


		private TreeNodeType nodeType;
		public TreeNodeType NodeType
		{
			get { return this.nodeType; }
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

		private void onSelectionStateChanged(bool? value)
		{
			if (this.nodeType == TreeNodeType.Folder)
			{
				this.updateDownwardSourceState(value);
			}
			this.updateUpwardSourceState();
		}

		private void updateDownwardSourceState(bool? value)
		{
			if (value.HasValue)
			{
				var folder = this.fileData as FolderData;
				if (folder.Source.Count > 0)
				{
					folder.Source.Foreach(item => item.TreeNode.setIsSelected(value.Value));
				}
			}
		}

		private void updateUpwardSourceState()
		{
			var parentFolder = this.fileData.ParentFolder;
			while (parentFolder != null)
			{
				int selectedCount = parentFolder.Source.Count(item =>
					!item.TreeNode.IsSelected.HasValue || item.TreeNode.IsSelected == true);
				if (selectedCount == 0)
					parentFolder.TreeNode.setIsSelected(false);
				else if (selectedCount == parentFolder.Source.Count)
					parentFolder.TreeNode.setIsSelected(true);
				else
					parentFolder.TreeNode.setIsSelected(null);

				parentFolder = parentFolder.ParentFolder;
			}
		}
	}
}
