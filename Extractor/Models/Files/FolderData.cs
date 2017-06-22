using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Models
{
	public class FolderData : Common.ViewModelBase
	{
		public FolderData(string name)
		{
			this.files = new List<FileData>();
			this.subFolders = new List<FolderData>();
			this.name = name;
			this.PropertyChanged += this.onPropertyChanged;
		}

		private List<FileData> files;
		public List<FileData> Files
		{
			get { return this.files; }
		}

		private List<FolderData> subFolders;
		public List<FolderData> SubFolders
		{
			get { return this.subFolders; }
		}

		private FolderData parentFolder;
		public FolderData ParentFolder
		{
			get { return this.parentFolder; }
		}

		private int level = 0;
		public int Level
		{
			get { return this.level; }
		}

		private string name;
		public string Name
		{
			get { return this.name; }
		}

		private bool? isSelected = false;
		public bool? IsSelected
		{
			get { return this.isSelected; }
			set
			{
				if (this.isSelected != value)
				{
					this.isSelected = value;
					this.NotifyPropertyChangedAsync("IsSelected");
				}
			}
		}

		private Common.AutoInvokeObservableCollection<object> source =
			new Common.AutoInvokeObservableCollection<object>();
		public Common.AutoInvokeObservableCollection<object> Source
		{
			get { return this.source; }
		}

		public void Add(FolderData subFolder)
		{
			if (subFolder != null)
			{
				this.subFolders.Add(subFolder);
				subFolder.parentFolder = this;
				subFolder.level++;

				this.source.Add(subFolder);
			}
		}

		public void Remove(FolderData subFolder)
		{
			if (subFolder != null && this.subFolders.Contains(subFolder))
			{
				if (this.subFolders.Remove(subFolder))
				{
					subFolder.parentFolder = null;
					subFolder.level--;

					this.source.Remove(subFolder);
				}
			}
		}

		public void Add(FileData file)
		{
			if (file != null)
			{
				this.files.Add(file);
				file.ParentFolder = this;
				file.PropertyChanged += this.onPropertyChanged;

				this.source.Add(file);
			}
		}

		public void RemoveFile(FileData file)
		{
			if (file != null && this.files.Contains(file))
			{
				if (this.files.Remove(file))
				{
					file.ParentFolder = null;
					file.PropertyChanged -= this.onPropertyChanged;

					this.source.Remove(file);
				}
			}
		}

		private void onPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsSelected")
			{
				if (object.ReferenceEquals(sender, this))
				{
					this.updateChildrenIsSelectedStates(this.isSelected);
				}
				this.updateIsSelectedStates();
			}
		}

		private void updateChildrenIsSelectedStates(bool? value)
		{
			if (value.HasValue)
			{
				this.files.ForEach(file => file.IsSelected = value.Value);
				this.subFolders.ForEach(folder => folder.IsSelected = value.Value);
			}
		}

		private void updateIsSelectedStates()
		{
			if (this.source.Count == 0)
			{
				if (!this.isSelected.HasValue)
				{
					this.IsSelected = false;
				}
			}
			else
			{
				int selectedCount = 0;
				foreach (var item in this.source)
				{
					if (item is FileData && (item as FileData).IsSelected)
					{
						selectedCount++;
					}
					else if (item is FolderData)
					{
						var folder = item as FolderData;
						if (folder.IsSelected.HasValue && folder.IsSelected.Value)
						{
							selectedCount++;
						}
					}
				}

				if (selectedCount == 0)
				{
					this.IsSelected = false;
				}
				else if (selectedCount == this.source.Count)
				{
					this.IsSelected = true;
				}
				else
				{
					this.IsSelected = null;
				}
			}
		}
	}
}
