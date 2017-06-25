using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public abstract class FileDataBase : Common.ViewModelBase
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

		public abstract bool CanMoveTo(FileDataBase destination);

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
