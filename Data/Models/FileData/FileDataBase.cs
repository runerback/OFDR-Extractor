using System;

namespace Extractor.Data
{
	public class FileDataBase : Common.ViewModelBase
	{
		protected FileDataBase() { }

		protected string name;
		public string Name
		{
			get { return this.name; }
		}

		public FolderData ParentFolder { get; set; }

		protected TreeNodeModel treeNode;
		public TreeNodeModel TreeNode
		{
			get { return this.treeNode; }
		}

		public bool IsChecked
		{
			get
			{
				return this.treeNode.IsSelected.HasValue ?
					this.treeNode.IsSelected.Value :
					false;
			}
		}

		public virtual bool CanMoveTo(FileDataBase destination)
		{
			if (destination.TreeNode.NodeType == TreeNodeType.File)
			{
				if (destination.ParentFolder == this.ParentFolder)
					return false;
			}
			else if (destination.TreeNode.NodeType == TreeNodeType.Folder)
			{
				FileDataBase ancestor = destination;
				while ((ancestor = ancestor.ParentFolder) != null)
				{
					if (ancestor == this)
						return false;
				}
				if (destination == this.ParentFolder)
					return false;
			}
			return true;
		}

		public virtual void MoveTo(FileDataBase destination)
		{
			if (destination.TreeNode.NodeType == TreeNodeType.File)
			{
				destination.ParentFolder.Add(this);
			}
			else if (destination.TreeNode.NodeType == TreeNodeType.Folder)
			{
				(destination as FolderData).Add(this);
			}
			else
			{
				throw new NotImplementedException(destination.TreeNode.NodeType.ToString());
			}
			this.ParentFolder.Remove(this);
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
