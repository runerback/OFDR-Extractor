using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Data
{
	public class MenuItemModel : Common.ViewModelBase
	{
		public MenuItemModel() { }

		public MenuItemModel(string title, string description)
		{
			this.title = title;
			this.description = description;
		}

		public MenuItemModel(string title) : this(title, title) { }

		private string title;
		public string Title
		{
			get { return this.title; }
			set
			{
				if (this.title != value)
				{
					this.title = value;
					this.NotifyPropertyChanged("Title");
				}
			}
		}

		private string description;
		public string Description
		{
			get { return this.description; }
			set
			{
				if (this.description != value)
				{
					this.description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		private Common.AutoInvokeObservableCollection<MenuItemModel> subMenuItems =
			new Common.AutoInvokeObservableCollection<MenuItemModel>();
		public Common.AutoInvokeObservableCollection<MenuItemModel> SubMenuItems
		{
			get { return this.subMenuItems; }
		}

		public bool HasSubMenus
		{
			get { return this.subMenuItems.Count > 0; }
		}

		private System.Windows.Input.ICommand command;
		public System.Windows.Input.ICommand Command
		{
			get { return this.command; }
			set { this.command = value; }
		}
	}
}
