using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Extractor.Presentation.ViewModel
{
	public class FileMapMenuViewModel : ContextMenuViewModelBase
	{
		protected override void buildMenu()
		{
			var saveAs = new Data.MenuItemModel("Save as...", "Save current architecture to xml")
			{
				Command = new Common.RelayCommand(this.saveMap)
			};

			this.menuItems.Add(saveAs);
		}

		private void saveMap(object obj)
		{
			System.Windows.MessageBox.Show("Not implement yet");
		}
	}
}
