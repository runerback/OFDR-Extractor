using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Extractor.Models
{
	public class FileData : FileDataBase
	{
	//	public FileData(LZSS.FileItem lzssFileInfo)
	//	{
	//		if (lzssFileInfo == null)
	//		{
	//			throw new ArgumentNullException("lzssFileInfo");
	//		}
	//		if (lzssFileInfo.Size == 0)
	//		{
	//			throw new ArgumentException("lzssFileInfo. seems folder");
	//		}
	//		this.name = lzssFileInfo.Name;
	//		this.size = lzssFileInfo.Size;
	//		this.formattedSize = formatSize(lzssFileInfo.Size);
	//		this.outputName = string.Format("{0}{1}", lzssFileInfo.Name, lzssFileInfo.Index < 1 ? "" : lzssFileInfo.Index.ToString());
	//		this.index = lzssFileInfo.Index;
	//		this.treeNode = new TreeNodeModel(this);
	//	}

		public FileData(IFileInfo outerFile)
		{
			if (outerFile == null)
			{
				throw new ArgumentNullException("outerFile");
			}
			if (outerFile.Size == 0)
			{
				throw new ArgumentException("lzssFileInfo. seems folder");
			}
			this.name = outerFile.Name;
			this.size = outerFile.Size;
			this.formattedSize = formatSize(outerFile.Size);
			int index = outerFile.Index;
			this.outputName = string.Format("{0}{1}", outerFile.Name, index < 1 ? "" : index.ToString());
			this.index = index;
			this.treeNode = new TreeNodeModel(this);
		}

		private long size;
		public long Size
		{
			get { return this.size; }
		}

		private int index;
		public int Index
		{
			get { return this.index; }
		}

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
		/// <summary>
		/// output file name with Index which is DAT.exe actually output filename
		/// </summary>
		public string OutputName
		{
			get { return this.outputName; }
		}

		/// <summary>
		/// output file fullname without Index
		/// </summary>
		public string OutputFullName
		{
			get
			{
				string outputFolder = this.getOutputFolderPath();
				return string.Concat(outputFolder, this.name);
			}
		}

		private string getOutputFolderPath()
		{
			if (this.ParentFolder == null) return null;

			Stack<string> upperFolders = new Stack<string>();
			var folder = this.ParentFolder;
			while (folder != null)
			{
				upperFolders.Push(folder.Name);
				folder = folder.ParentFolder;
			}

			if (upperFolders.Count == 0) return null;

			StringBuilder folderPathBuilder = new StringBuilder("/");
			while (upperFolders.Count > 0)
			{
				folderPathBuilder.AppendFormat("{0}/", upperFolders.Pop());
			}
			return folderPathBuilder.ToString();
		}

		public override string ToString()
		{
			return this.OutputName;
		}
	}
}
