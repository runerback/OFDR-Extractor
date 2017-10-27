using System.Windows;
using System.Windows.Controls;

namespace Extractor.Shell.Controls
{
	public class FolderDataPersenter : TreeViewItem
	{
		public FolderDataPersenter(ContextMenu folderContextMenu)
		{
			VirtualizingStackPanel.SetIsVirtualizing(this, true);
			this.Focusable = false;
			this.folderContextMenu = folderContextMenu;
			this.ContextMenu = folderContextMenu;
		}

		private ContextMenu folderContextMenu;

		protected override System.Windows.DependencyObject GetContainerForItemOverride()
		{
			return new FolderDataPersenter(this.folderContextMenu);
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FolderDataPersenter;
		}
	}
}
