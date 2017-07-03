using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Data
{
	public class AppSetting : Common.ViewModelBase, IUIModel<AppSetting>
	{
		#region OFDRRootFolder
		private string ofdrRootFolder;
		public string OFDRRootFolder
		{
			get { return this.ofdrRootFolder; }
			set
			{
				if (this.ofdrRootFolder != value)
				{
					this.ofdrRootFolder = value;
					this.NotifyPropertyChanged("OFDRRootFolder");
				}
			}
		}

		public static readonly string OFDRRootFolderSectionName = "OFDRRootFolder";
		public static readonly string OFDRRootFolderDefaultValue = ".\\Resources";
		#endregion

		#region DATFilePath
		private string datFilePath;
		public string DATFilePath
		{
			get { return this.datFilePath; }
			set
			{
				if (this.datFilePath != value)
				{
					this.datFilePath = value;
					this.NotifyPropertyChanged("DATFilePath");
				}
			}
		}

		public static readonly string DATFilePathSectionName = "DATFilePath";
		public static readonly string DATFilePathDefaultValue = ".\\Resources\\dat.exe";
		#endregion DATFilePath

		#region OutputFolder
		private string outputFolder;
		public string OutputFolder
		{
			get { return this.outputFolder; }
			set
			{
				if (this.outputFolder != value)
				{
					this.outputFolder = value;
					this.NotifyPropertyChanged("OutputFolder");
				}
			}
		}

		public static readonly string OutputFolderSectionName = "OutputFolder";
		public static readonly string OutputFolderDefaultValue = ".\\Resources\\unpacked";
		#endregion OutputFolder

		#region MaxUnpackingParallelismCount
		private int maxUnpackingParallelismCount;
		public int MaxUnpackingParallelismCount
		{
			get { return this.maxUnpackingParallelismCount; }
			set
			{
				if (this.maxUnpackingParallelismCount != value)
				{
					this.maxUnpackingParallelismCount = value;
					this.NotifyPropertyChanged("MaxUnpackingParallelismCount");
				}
			}
		}

		public static readonly string MaxUnpackingParallelismCountSectionName = "MaxUnpackingParallelismCount";
		public static readonly int MaxUnpackingParallelismCountDefaultValue = 1;
		#endregion MaxUnpackingParallelismCount

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
