using System.Windows.Controls;

namespace Extractor.Shell.Controls
{
	public class FileMapPresenter : TreeView
	{
		public FileMapPresenter()
		{
			VirtualizingStackPanel.SetIsVirtualizing(this, true);

			ScrollViewer.SetHorizontalScrollBarVisibility(this, ScrollBarVisibility.Auto);
			ScrollViewer.SetVerticalScrollBarVisibility(this, ScrollBarVisibility.Auto);
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
