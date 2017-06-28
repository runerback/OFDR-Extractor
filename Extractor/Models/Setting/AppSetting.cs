using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Models
{
	internal class AppSetting : IUIModel<AppSetting>
	{
		public string OFDRRootFolder { get; set; }
		public static readonly string OFDRRootFolderSectionName = "OFDRRootFolder";
		public static readonly string OFDRRootFolderDefaultValue = ".\\Resources";

		public string DATFilePath { get; set; }
		public static readonly string DATFilePathSectionName = "DATFilePath";
		public static readonly string DATFilePathDefaultValue = ".\\Resources\\dat.exe";

		public string OutputFolder { get; set; }
		public static readonly string OutputFolderSectionName = "OutputFolder";
		public static readonly string OutputFolderDefaultValue = ".\\Resources\\unpacked";

		public int MaxUnpackingParallelismCount { get; set; }
		public static readonly string MaxUnpackingParallelismCountSectionName = "MaxUnpackingParallelismCount";
		public static readonly int MaxUnpackingParallelismCountDefaultValue = 1;

		public AppSetting Clone()
		{
			AppSetting copy = new AppSetting();
			copy.UpdateBy(this);
			return copy;
		}

		public void UpdateBy(AppSetting other)
		{
			if (other == null)
			{
				throw new ArgumentException("AppSetting.UpdateBy: other");
			}

			this.OFDRRootFolder = other.OFDRRootFolder;
			this.DATFilePath = other.DATFilePath;
			this.OutputFolder = other.OutputFolder;
			this.MaxUnpackingParallelismCount = other.MaxUnpackingParallelismCount;
		}
	}
}
