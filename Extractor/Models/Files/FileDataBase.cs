using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
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

		public override string ToString()
		{
			return this.Name;
		}
	}
}
