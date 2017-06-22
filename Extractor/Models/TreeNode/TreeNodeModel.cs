using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	public class TreeNodeModel : Common.ViewModelBase
	{
		public bool IsChecked { get; set; }

		private TreeNodeType nodeType;
		public TreeNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}


	}
}
