using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Extractor.Presentation.ViewModel
{
	public partial class SettingsViewModel : Common.ValidationViewModelBase
	{
		public SettingsViewModel()
		{
			this.readFromSettings(Business.ConfigManager.CurrentSettingsCopy);
			this.chooseOFDRRootFolderCommand = new Common.RelayCommand(this.chooseOFDRRootFolder);
			this.chooseDATFilePathCommand = new Common.RelayCommand(this.chooseDATFilePath);
			this.chooseOutputFolderCommand = new Common.RelayCommand(this.chooseOutputFolder);
		}

		private void readFromSettings(Data.AppSetting appSettings)
		{
			this.OFDRRootFolder = appSettings.OFDRRootFolder;
			this.DATFilePath = appSettings.DATFilePath;
			this.OutputFolder = appSettings.OutputFolder;
			this.MaxUnpackingParallelismCount = appSettings.MaxUnpackingParallelismCount.ToString();
		}

		private void writeToSettings()
		{
			Business.ConfigManager.OFDRRootFolder = this.OFDRRootFolder;
			Business.ConfigManager.DATFilePath = this.DATFilePath;
			Business.ConfigManager.OutputFolder = this.OutputFolder;
			Business.ConfigManager.MaxUnpackingParallelismCount = this.maxUnpackingParallelismCount.Value;

			Business.ConfigManager.SaveChanges();
		}

		private Common.Validation.ValidationStringModel ofdrRootFolder = new Common.Validation.ValidationStringModel();
		public string OFDRRootFolder
		{
			get { return this.ofdrRootFolder.StringValue; }
			private set
			{
				if (this.ofdrRootFolder.StringValue != value)
				{
					this.ofdrRootFolder.StringValue = value;
					this.NotifyPropertyChanged("OFDRRootFolder");
				}
			}
		}

		private Common.RelayCommand chooseOFDRRootFolderCommand;
		public Common.RelayCommand ChooseOFDRRootFolderCommand
		{
			get { return this.chooseOFDRRootFolderCommand; }
		}

		private void chooseOFDRRootFolder(object obj)
		{
			string path = Common.Dialog.FileSystemDialog.ChooseFolder("Choose OFDR installed folder");
			if (!string.IsNullOrEmpty(path))
			{
				this.OFDRRootFolder = path;
			}
		}

		private Common.Validation.ValidationStringModel datFilePath = new Common.Validation.ValidationStringModel();
		public string DATFilePath
		{
			get { return this.datFilePath.StringValue; }
			private set
			{
				if (this.datFilePath.StringValue != value)
				{
					this.datFilePath.StringValue = value;
					this.NotifyPropertyChanged("DATFilePath");
				}
			}
		}

		private Common.RelayCommand chooseDATFilePathCommand;
		public Common.RelayCommand ChooseDATFilePathCommand
		{
			get { return this.chooseDATFilePathCommand; }
		}

		private void chooseDATFilePath(object obj)
		{
			string path = Common.Dialog.FileSystemDialog.ChooseFile("Choose dat.exe file");
			if (!string.IsNullOrEmpty(path))
			{
				this.DATFilePath = path;
			}
		}

		private Common.Validation.ValidationStringModel outputFolder = new Common.Validation.ValidationStringModel();
		public string OutputFolder
		{
			get { return this.outputFolder.StringValue; }
			private set
			{
				if (this.outputFolder.StringValue != value)
				{
					this.outputFolder.StringValue = value;
					this.NotifyPropertyChanged("OutputFolder");
				}
			}
		}

		private Common.RelayCommand chooseOutputFolderCommand;
		public Common.RelayCommand ChooseOutputFolderCommand
		{
			get { return this.chooseOutputFolderCommand; }
		}

		private void chooseOutputFolder(object obj)
		{
			string path = Common.Dialog.FileSystemDialog.ChooseFolder("Choose output folder");
			if (!string.IsNullOrEmpty(path))
			{
				this.OutputFolder = path;
			}
		}

		private Common.Validation.ValidationIntModel maxUnpackingParallelismCount = new Common.Validation.ValidationIntModel();
		public string MaxUnpackingParallelismCount
		{
			get { return this.maxUnpackingParallelismCount.StringValue; }
			set
			{
				if (this.maxUnpackingParallelismCount.StringValue != value)
				{
					this.maxUnpackingParallelismCount.StringValue = value;
					this.NotifyPropertyChanged("MaxUnpackingParallelismCount");
				}
			}
		}
	}
}
