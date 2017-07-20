using System.Threading.Tasks;

namespace Extractor.Presentation.ViewModel
{
	public abstract class ContextMenuViewModelBase : Common.ViewModelBase
	{
		protected ContextMenuViewModelBase()
		{
			Task.Factory.StartNew(buildMenu)
				.ContinueWith(t => NotifyPropertyChanged("MenuItems"));
		}

		protected Common.AutoInvokeObservableCollection<Data.MenuItemModel> menuItems =
			   new Common.AutoInvokeObservableCollection<Data.MenuItemModel>();
		public Common.AutoInvokeObservableCollection<Data.MenuItemModel> MenuItems
		{
			get { return this.menuItems; }
		}

		protected abstract void buildMenu();
	}
}
