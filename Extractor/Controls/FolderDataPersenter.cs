using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;

namespace Extractor.Controls
{
	public class FolderDataPersenter : TreeViewItem
	{
		public FolderDataPersenter()
		{
			VirtualizingStackPanel.SetIsVirtualizing(this, false);

			this.Dispatcher.BeginInvoke(
				(Action)this.setExpandingEventListener,
				 System.Windows.Threading.DispatcherPriority.Loaded);
		}

		private void setExpandingEventListener()
		{
			if (this.Template != null)
			{
				var expander = this.Template.FindName("Expander", this) as ToggleButton;
				if (expander != null)
				{
					expander.PreviewMouseLeftButtonDown += this.onExpanderPreviewMouseLeftButtonDown;
				}
			}
		}

		private void onExpanderPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			e.Handled = true;

			this.Dispatcher.BeginInvoke((Action)delegate
			{
				this.IsExpanded = !this.IsExpanded; // no work here
			}, System.Windows.Threading.DispatcherPriority.Render);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Business.TreeViewExpandingCounter.StepForward();
		}

		protected override void OnExpanded(RoutedEventArgs e)
		{
			var folder = this.DataContext as Models.FolderData;
			if (folder != null)
			{
				if (folder.SubFolders.Count > 100)
				{
					Business.TreeViewExpandingCounter.Finish += this.onExpandingCounterDone;
					Business.TreeViewExpandingCounter.Set(folder.SubFolders.Count);
				}
			}
			base.OnExpanded(e);
		}

		private void onExpandingCounterDone()
		{
			Business.TreeViewExpandingCounter.Finish -= this.onExpandingCounterDone;
		}
	}
}
