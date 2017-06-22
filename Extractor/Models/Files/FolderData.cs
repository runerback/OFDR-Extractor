using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extractor.Models
{
	public class FolderData : FileDataBase
	{
		public FolderData(string name)
		{
			this.files = new List<FileData>();
			this.subFolders = new List<FolderData>();
			this.name = name;
			//this.PropertyChanged += this.onPropertyChanged;
			//this.itemsGenerated = new Common.DelegateCommand(this.onItemsGenerated);
			this.treeNode = new TreeNodeModel(this);
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

		//private FolderData parentFolder;
		//public FolderData ParentFolder
		//{
		//	get { return this.parentFolder; }
		//}

		private int level = 0;
		public int Level
		{
			get { return this.level; }
		}

		//private string name;
		//public string Name
		//{
		//	get { return this.name; }
		//}

		//private bool? isSelected = false;
		//public bool? IsSelected
		//{
		//	get { return this.isSelected; }
		//	set
		//	{
		//		bool? handledValue;
		//		if (!this.enableThreeState && !value.HasValue)
		//		{
		//			handledValue = false;
		//		}
		//		else
		//		{
		//			handledValue = value;
		//		}

		//		if (this.isSelected != handledValue)
		//		{
		//			this.isSelected = handledValue;
		//			this.NotifyPropertyChanged("IsSelected");
		//		}
		//	}
		//}

		private bool enableThreeState = false;
		private bool handlingSelectStateChanged = true;

		private Visibility visibility;
		public Visibility Visibility
		{
			get { return this.visibility; }
			set
			{
				if (this.visibility != value)
				{
					this.visibility = value;
					this.NotifyPropertyChanged("Visibility");
				}
			}
		}

		//private bool isExpanding;
		//public bool IsExpanding
		//{
		//	get { return this.isExpanding; }
		//	set
		//	{
		//		if (this.isExpanding != value)
		//		{
		//			this.isExpanding = value;

		//			if (value && this.subFolders.Count > 0)
		//			{
		//				this.Visibility = System.Windows.Visibility.Hidden;
		//			}
		//			else
		//			{
		//				this.Visibility = System.Windows.Visibility.Visible;
		//			}
		//		}
		//	}
		//}

		//private bool isExpanded;
		//public bool IsExpanded
		//{
		//	get { return this.isExpanded; }
		//	set
		//	{
		//		if (this.isExpanded != value)
		//		{
		//			this.isExpanded = value;
		//			this.NotifyPropertyChanged("IsExpanded");
		//		}
		//	}
		//}

		//private Common.DelegateCommand itemsGenerated;
		//public Common.DelegateCommand ItemsGenerated
		//{
		//	get { return this.itemsGenerated; }
		//}

		//private void onItemsGenerated(object obj)
		//{
		//	this.Visibility = System.Windows.Visibility.Visible;
		//}

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
				subFolder.ParentFolder = this;
				subFolder.level = this.level + 1;

				this.source.Add(subFolder);
			}
		}

		public void Remove(FolderData subFolder)
		{
			if (subFolder != null && this.subFolders.Contains(subFolder))
			{
				if (this.subFolders.Remove(subFolder))
				{
					subFolder.ParentFolder = null;
					subFolder.level = -1;

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
				//file.PropertyChanged += this.onPropertyChanged;

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
					//file.PropertyChanged -= this.onPropertyChanged;

					this.source.Remove(file);
				}
			}
		}

		//private void onPropertyChanged(object sender, PropertyChangedEventArgs e)
		//{
		//	if (e.PropertyName == "IsSelected")
		//	{
		//		if (this.handlingSelectStateChanged)
		//		{
		//			if (object.ReferenceEquals(sender, this))
		//			{
		//				this.updateChildrenIsSelectedStates(this.isSelected);
		//			}
		//			this.updateIsSelectedStates(this);
		//		}
		//	}
			
		//	//if (e.PropertyName == "Visibility")
		//	//{
		//	//	this.updateVirtualizedSource();
		//	//}
		//}

		//private void updateChildrenIsSelectedStates(bool? value)
		//{
		//	if (value.HasValue)
		//	{
		//		this.files.ForEach(file => file.IsSelected = value.Value);
		//		this.subFolders.ForEach(folder => folder.IsSelected = value.Value);
		//	}
		//}

		//private void updateIsSelectedStates(FolderData currentFolder)
		//{
		//	if (currentFolder.source.Count == 0)
		//	{
		//		if (!currentFolder.isSelected.HasValue)
		//		{
		//			currentFolder.IsSelected = false;
		//		}
		//	}
		//	else
		//	{
		//		int selectedCount = 0;
		//		foreach (var item in currentFolder.source)
		//		{
		//			if (item is FileData && (item as FileData).IsSelected)
		//			{
		//				selectedCount++;
		//			}
		//			else if (item is FolderData)
		//			{
		//				var folder = item as FolderData;
		//				if (folder.IsSelected.HasValue && folder.IsSelected.Value)
		//				{
		//					selectedCount++;
		//				}
		//			}
		//		}

		//		var current = currentFolder;
		//		while (current != null)
		//		{
		//			updateIsSelectedStates(current, selectedCount);
		//			current = current.parentFolder;
		//		}
		//	}
		//}

		//private static void updateIsSelectedStates(FolderData currentFolder, int selectedCount)
		//{
		//	if (selectedCount == 0)
		//	{
		//		currentFolder.IsSelected = false;
		//	}
		//	else if (selectedCount == currentFolder.source.Count)
		//	{
		//		currentFolder.IsSelected = true;
		//	}
		//	else
		//	{
		//		currentFolder.enableThreeState = true;
		//		currentFolder.handlingSelectStateChanged = false;
		//		currentFolder.IsSelected = null;
		//		currentFolder.enableThreeState = false;
		//		currentFolder.handlingSelectStateChanged = true;
		//	}
		//}

		#region virtualizing
		//private Common.AutoInvokeObservableCollection<object> virtualizedSource = new Common.AutoInvokeObservableCollection<object>();
		//public Common.AutoInvokeObservableCollection<object> VirtualizedSource
		//{
		//	get { return this.virtualizedSource; }
		//}

		//private void updateVirtualizedSource()
		//{
		//	try
		//	{
		//		if (this.parentFolder != null)
		//		{
		//			int upperBound = -1;
		//			int lowerBound = -1;
		//			this.parentFolder.source.Foreach((item, i) =>
		//			{
		//				if (item is FolderData)
		//				{
		//					var folder = item as FolderData;
		//					if (upperBound < 0 && folder.isVisible)
		//					{
		//						upperBound = i;
		//						return;
		//					}
		//					if (lowerBound < 0 && !folder.isVisible)
		//					{
		//						lowerBound = i;
		//					}
		//				}
		//			});
		//		}

		//	}
		//	catch
		//	{
		//		throw;
		//	}
		//}
		#endregion
	}
}
