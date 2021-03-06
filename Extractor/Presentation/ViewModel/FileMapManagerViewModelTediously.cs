﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Presentation.ViewModel
{
    public class FileMapManagerViewModelTediously : Common.ViewModelBase
    {
		public FileMapManagerViewModelTediously()
		{
			Task.Factory.StartNew(this.readLzssFileMap);
		}

		private void readLzssFileMap()
		{
			try
			{
				Business.DATManagerSingleton.DataReceived += this.onDatDataReceived;
				Business.DATManagerSingleton.Exited += this.DatExisted;
				Business.DATManagerSingleton.Call();
			}
			catch
			{
				throw;
			}
		}

		private void onDatDataReceived(int processID, string data)
		{
			if (!this.isRootFolderGenerated)
			{
				this.generateRootFolder(data);
				this.isRootFolderGenerated = true;
			}
		}

		private void DatExisted(int processID)
		{
			Business.DATManagerSingleton.DataReceived -= this.onDatDataReceived;
			Business.DATManagerSingleton.Exited -= this.DatExisted;
		}

		private bool isRootFolderGenerated = false;

		private Data.FolderData rootFolder = null;
		public Data.FolderData Root
		{
			get { return this.rootFolder; }
		}

		private Common.AutoInvokeObservableCollection<Data.FolderData> rootSource =
			new Common.AutoInvokeObservableCollection<Data.FolderData>();
		public Common.AutoInvokeObservableCollection<Data.FolderData> RootSource
		{
			get { return this.rootSource; }
		}

		private void generateRootFolder(string data)
		{
			try
			{
				this.rootSource.Clear();
				Data.FolderData root = null;
				if (!string.IsNullOrWhiteSpace(data))
				{
					root = Business.LZSSFileMapParser.Parse(data);
				}

				if (root == null)
				{
					MessageBox.Show(data, "Cannot get file data from nfs file");
				}
				else
				{
					Data.FolderData fakeFolder = new Data.FolderData("test");
					fakeFolder.Add(new Data.FileData(new Data.LZSSFileItem()
					{
						Name = "test.txt",
						Index = 0,
						Size = 100
					}));
					fakeFolder.Add(new Data.FileData(new Data.LZSSFileItem()
					{
						Name = "test.txt",
						Index = 1,
						Size = 100
					}));
					root.SubFolders[0].Add(fakeFolder);

					this.rootFolder = root;
					this.rootSource.Add(root);
					this.NotifyPropertyChanged("RootSource");
				}
			}
			catch
			{
				throw;
			}
		}
    }
}
