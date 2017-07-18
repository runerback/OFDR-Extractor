using System.Windows.Controls;
using System.Windows;
using Extractor.Extension;

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
		}

		protected override System.Windows.DependencyObject GetContainerForItemOverride()
		{
			return new FolderDataPersenter();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FolderDataPersenter;
		}

		private ScrollViewer _scrollViewer;
		private void onLoaded(object sender, RoutedEventArgs e)
		{
			this.Loaded -= onLoaded;
			this._scrollViewer = this.FindVisualChild<ScrollViewer>();
		}

		private bool isDragging;
		internal bool IsDragging
		{
			get { return this.isDragging; }
			set
			{
				if (value != this.isDragging)
				{
					this.isDragging = value;
				}
			}
		}

		private const double tolerance = 36;
		private const double step_length = 16;

		protected override void OnPreviewMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			if (this.isDragging)
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
			else
			{
				base.OnPreviewMouseMove(e);
			}
		}
	}
}
