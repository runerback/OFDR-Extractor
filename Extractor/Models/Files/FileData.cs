﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Extractor.Models
{
	public class FileData : FileDataBase
	{
		public FileData(LZSS.FileItem lzssFileInfo)
		{
			if (lzssFileInfo == null)
			{
				throw new ArgumentNullException("lzssFileInfo");
			}
			if (lzssFileInfo.Size == 0)
			{
				throw new ArgumentException("lzssFileInfo. seems folder");
			}
			this.name = lzssFileInfo.Name;
			this.size = lzssFileInfo.Size;
			this.formattedSize = formatSize(lzssFileInfo.Size);
			this.outputName = string.Format("{0}{1}", lzssFileInfo.Name, lzssFileInfo.Index < 1 ? "" : lzssFileInfo.Index.ToString());
			//this.index = string.Format(" - {0}", lzssFileInfo.Index + 1);
			this.treeNode = new TreeNodeModel(this);
		}

		private long size;
		public long Size
		{
			get { return this.size; }
		}

		//private string index;
		//public string Index
		//{
		//	get { return this.index; }
		//}

		private string formattedSize;
		public string FormattedSize
		{
			get { return this.formattedSize; }
		}

		private static string formatSize(long size)
		{
			List<char> units = new List<char>()
			{
				'B', 'K', 'M', 'G', 'T'
			};
			int level = 0;
			while (true && level < units.Count - 1)
			{
				if (size < 1024)
				{
					if (size < 1000)
					{
						break;
					}
					else
					{
						size = 1;
						level++;
						break;
					}
				}
				else
				{
					size = size / 1024;
				}
				level++;
			}
			return string.Format("{0} {1}", size, units[level]);
		}

		private string outputName;
		public string OutputName
		{
			get { return this.outputName; }
		}

		public override string ToString()
		{
			return this.OutputName;
		}

		public override bool CanMoveTo(FileDataBase destination)
		{
			if (destination.TreeNode.NodeType == TreeNodeType.File)
			{
				if (destination.ParentFolder == this.ParentFolder)
					return false;
			}
			else if (destination.TreeNode.NodeType == TreeNodeType.Folder)
			{
				if (destination as FolderData == this.ParentFolder)
					return false;
			}
			return true;
		}
	}
}
