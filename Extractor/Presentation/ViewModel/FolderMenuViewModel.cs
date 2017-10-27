using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Presentation.ViewModel
{
	public class FolderMenuViewModel : ContextMenuViewModelBase
	{
		protected override void buildMenu()
		{
			var openInNewTab = new Data.MenuItemModel("Scope...", "Open folder in new tab")
			{
				Command = new Common.RelayCommand(this.openInNewTab)
			};

			this.menuItems.Add(openInNewTab);
		}

		private void openInNewTab(object obj)
		{
			//cannot get FolderData here...
			var module = new PageNavigator.Model.ModuleData("hierarchy", "aaa");
			PageNavigator.Business.ViewModels.MenuContainerViewModel.Open(module);
		}
	}
}
