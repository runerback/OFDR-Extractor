using System.Windows.Controls;
using System.Windows;
using Extractor.Extension;
using System;
using System.Windows.Input;

namespace Extractor.Shell.Controls
{
	public class FileMapPresenter : TreeView
	{
		public FileMapPresenter()
		{
			VirtualizingStackPanel.SetIsVirtualizing(this, true);

			ScrollViewer.SetHorizontalScrollBarVisibility(this, ScrollBarVisibility.Auto);
			ScrollViewer.SetVerticalScrollBarVisibility(this, ScrollBarVisibility.Auto);

			ScrollViewer.SetCanContentScroll(this, false);

			this.Loaded += onLoaded;

			//this.AllowDrop = true;
		}

		protected override System.Windows.DependencyObject GetContainerForItemOverride()
		{
			return new FolderDataPersenter();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FolderDataPersenter;
		}

		protected override void OnPreviewDragOver(DragEventArgs e)
		{
			updateScrollerWhenDragging(e);
			base.OnPreviewDragOver(e);
		}

		private ScrollViewer _scrollViewer;
		private void onLoaded(object sender, RoutedEventArgs e)
		{
			this.Loaded -= onLoaded;
			this._scrollViewer = this.FindVisualChild<ScrollViewer>();
		}

		private const double tolerance = 36;
		private const double step_length = 16;

		private void updateScrollerWhenDragging(DragEventArgs e)
		{
			var scrollViewer = this._scrollViewer;

			double verticalPosition = e.GetPosition(scrollViewer).Y;
			if (verticalPosition < tolerance) //top, scroll up
			{
				if (scrollViewer.VerticalOffset > 0)
				{
					double targetOffset = scrollViewer.VerticalOffset - step_length;
					scrollViewer.ScrollToVerticalOffset(targetOffset < 0 ? 0 : targetOffset);
				}
			}
			else if (verticalPosition > scrollViewer.ActualHeight - tolerance) //bottom, scroll down
			{
				if (scrollViewer.VerticalOffset < scrollViewer.ScrollableHeight)
				{
					double targetOffset = scrollViewer.VerticalOffset + step_length;
					scrollViewer.ScrollToVerticalOffset(targetOffset > scrollViewer.ScrollableHeight ? scrollViewer.ScrollableHeight : targetOffset);
				}
			}
		}
	}
}
