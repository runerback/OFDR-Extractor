using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;

namespace Extractor.Business
{
	public static class ConfigManager
	{
		/// <summary>
		/// next step: when this call return false, load setting page and show items need to be set.
		/// </summary>
		/// <returns></returns>
		public static bool CheckConfigurations()
		{
			if (!checkOFDRRootFolder())
			{
				//CommonOpenFileDialog dialog = new CommonOpenFileDialog()
				//{
				//	Title = "Choose OFDR root folder",
				//	AllowPropertyEditing = false,
				//	EnsureFileExists = false,
				//	EnsurePathExists = true,
				//	IsFolderPicker = true,
				//	Multiselect = false
				//};
				//if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				//{
				//	if (checkOFDRRootFolder(dialog.FileName))
				//	{
				//		OFDRRootFolder = dialog.FileName;
				//		return true;
				//	}
				//}
				MessageBox.Show("Please choose a valid OFDR installed root folder. Application will now exit.", "Warning");
				return false;
			}
			if (!checkDATFilePath())
			{
				MessageBox.Show("dat.exe file is missing. Application will now exit.", "Warning");
				return false;
			}
			if (!checkOutputFolder())
			{
				MessageBox.Show("output folder not set. Application will now exit.", "Warning");
				return false;
			}
			if (!checkMaxUnpackingParallelismCount())
			{
				MessageBox.Show("max unpacking parallelism count not set. Application will now exit.", "Warning");
				return false;
			}

			return true;
		}

		private static Models.AppSetting settings = new Models.AppSetting();
		internal static Models.AppSetting CurrentSettingsCopy
		{
			get { return settings.Clone(); }
		}

		#region OFDRRoot
		private static bool checkOFDRRootFolder()
		{
			try
			{
				var ofdrRootFolderSection = Properties.Settings.Default[Models.AppSetting.OFDRRootFolderSectionName];
				if (ofdrRootFolderSection == null)
				{
					Properties.Settings.Default.Properties.Add(
						new System.Configuration.SettingsProperty(
							Models.AppSetting.OFDRRootFolderSectionName,
							settings.OFDRRootFolder.GetType(),
							new System.Configuration.LocalFileSettingsProvider(),
							false,
							Models.AppSetting.OFDRRootFolderDefaultValue,
							System.Configuration.SettingsSerializeAs.String,
							new System.Configuration.SettingsAttributeDictionary(),
							false,
							true));
				}

				string ofdrRootFolder = ofdrRootFolderSection as string;
				if (!string.IsNullOrWhiteSpace(ofdrRootFolder))
				{
					var ofdrRootDir = new DirectoryInfo(Path.GetFullPath(ofdrRootFolder));
					if (ofdrRootDir.Exists)
					{
						var ofdrMain = ofdrRootDir.GetFiles("win_000.nfs", SearchOption.TopDirectoryOnly);
						if (ofdrMain != null && ofdrMain.Length == 1)
						{
							settings.OFDRRootFolder = ofdrRootDir.FullName;
							return true;
						}
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static string OFDRRootFolder
		{
			get { return settings.OFDRRootFolder; }
			set
			{
				if (value != settings.OFDRRootFolder)
				{
					settings.OFDRRootFolder = value;
					Properties.Settings.Default[Models.AppSetting.OFDRRootFolderSectionName] = value;
				}
			}
		}
		#endregion OFDRRoot

		#region DATPath
		private static bool checkDATFilePath()
		{
			try
			{
				var datFilePathSection = Properties.Settings.Default[Models.AppSetting.DATFilePathSectionName];
				if (datFilePathSection == null)
				{
					Properties.Settings.Default.Properties.Add(
						new System.Configuration.SettingsProperty(
							Models.AppSetting.DATFilePathSectionName,
							settings.DATFilePath.GetType(),
							new System.Configuration.LocalFileSettingsProvider(),
							false,
							Models.AppSetting.DATFilePathDefaultValue,
							System.Configuration.SettingsSerializeAs.String,
							new System.Configuration.SettingsAttributeDictionary(),
							false,
							true));
				}

				string datFilePath = datFilePathSection as string;
				if (!string.IsNullOrWhiteSpace(datFilePath))
				{
					var path = Path.GetFullPath(datFilePath);
					if (File.Exists(path))
					{
						DATFilePath = path;
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static string DATFilePath
		{
			get { return settings.DATFilePath; }
			set
			{
				if (value != settings.DATFilePath)
				{
					settings.DATFilePath = value;
					Properties.Settings.Default[Models.AppSetting.DATFilePathSectionName] = value;
				}
			}
		}
		#endregion DATPath

		#region OutputPath
		private static bool checkOutputFolder()
		{
			try
			{
				var outputFolderSection = Properties.Settings.Default[Models.AppSetting.OutputFolderSectionName];
				if (outputFolderSection == null)
				{
					Properties.Settings.Default.Properties.Add(
						new System.Configuration.SettingsProperty(
							Models.AppSetting.OutputFolderSectionName,
							settings.OutputFolder.GetType(),
							new System.Configuration.LocalFileSettingsProvider(),
							false,
							Models.AppSetting.OutputFolderDefaultValue,
							System.Configuration.SettingsSerializeAs.String,
							new System.Configuration.SettingsAttributeDictionary(),
							false,
							true));
				}

				string outputFolder = outputFolderSection as string;
				if (!string.IsNullOrWhiteSpace(outputFolder))
				{
					var outputDir = new DirectoryInfo(Path.GetFullPath(outputFolder));
					if (!outputDir.Exists)
					{
						outputDir.Create();
					}

					settings.OutputFolder = outputDir.FullName;
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static string OutputFolder
		{
			get { return settings.OutputFolder; }
			set
			{
				if (value != settings.OutputFolder)
				{
					settings.OutputFolder = value;
					Properties.Settings.Default[Models.AppSetting.OutputFolderSectionName] = value;
				}
			}
		}
		#endregion OutputPath

		#region MaxUnpackingParallelismCount

		private static bool checkMaxUnpackingParallelismCount()
		{
			try
			{
				var maxUnpackingParallelismCountSection = Properties.Settings.Default[Models.AppSetting.MaxUnpackingParallelismCountSectionName];
				if (maxUnpackingParallelismCountSection == null)
				{
					Properties.Settings.Default.Properties.Add(
						new System.Configuration.SettingsProperty(
							Models.AppSetting.MaxUnpackingParallelismCountSectionName,
							settings.MaxUnpackingParallelismCount.GetType(),
							new System.Configuration.LocalFileSettingsProvider(),
							false,
							Models.AppSetting.MaxUnpackingParallelismCountDefaultValue,
							System.Configuration.SettingsSerializeAs.String,
							new System.Configuration.SettingsAttributeDictionary(),
							false,
							true));
				}

				int maxUnpackingParallelismCount = (int)maxUnpackingParallelismCountSection;
				if (maxUnpackingParallelismCount > 0)
				{
					MaxUnpackingParallelismCount = maxUnpackingParallelismCount;
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static int MaxUnpackingParallelismCount
		{
			get { return settings.MaxUnpackingParallelismCount; }
			set
			{
				int handledValue = value > 0 ? value : 1;
				if (handledValue != settings.MaxUnpackingParallelismCount)
				{
					settings.MaxUnpackingParallelismCount = handledValue;
					Properties.Settings.Default[Models.AppSetting.MaxUnpackingParallelismCountSectionName] = handledValue;
				}
			}
		}

		#endregion MaxUnpackingParallelismCount

		public static void SaveChanges()
		{
			Properties.Settings.Default.Save();
		}
	}
}
