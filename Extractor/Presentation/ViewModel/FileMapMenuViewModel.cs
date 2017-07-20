using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extractor.Presentation.ViewModel
{
	public class FileMapMenuViewModel : ContextMenuViewModelBase
	{
		protected override void buildMenu()
		{
			var menuItem = new Data.MenuItemModel("Save as...", "Save current architecture to xml")
			{
				Command = new Common.RelayCommand(this.saveMap)
			};

			this.menuItems.Add(menuItem);
		}

		private void saveMap(object obj)
		{
			System.Windows.MessageBox.Show("Not implement yet");
		}
	}
}
