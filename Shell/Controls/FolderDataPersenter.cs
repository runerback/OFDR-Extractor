using System.Windows;
using System.Windows.Controls;

namespace Extractor.Shell.Controls
{
	public class FolderDataPersenter : TreeViewItem
	{
		public FolderDataPersenter()
		{
			VirtualizingStackPanel.SetIsVirtualizing(this, true);
			this.Focusable = false;
		}

		protected override System.Windows.DependencyObject GetContainerForItemOverride()
		{
			return new FolderDataPersenter();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FolderDataPersenter;
		}
	}
}
