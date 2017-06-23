using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Extractor.Controls
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

		#region drag & drop

		//private bool isFirstOnApplyTemplate = false;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (Business.ReferenceComparer.Left == null)
			{
				Business.ReferenceComparer.Left = this;
			}
			else
			{
				Business.ReferenceComparer.Right = this;
				bool equal = Business.ReferenceComparer.IsReferenceEqual(); 
				//false returned, so it's not the same Control between two call.
			}

			//if (this.Template != null)
			//{
			//	var headerContentPresenter= this.Template.FindName("PART_Header", this) as ContentPresenter;
			//	if (headerContentPresenter != null)
			//	{
			//		var headerContent = headerContentPresenter.Content;
					
			//	}
			//}

			//var contentPresenter = this.FindVisualChild<ContentPresenter>();
			//if (contentPresenter != null && contentPresenter.ContentTemplate != null)
			//{
			//	DataTemplate contentTemplate = contentPresenter.ContentTemplate;
			//	var dragableGizmo = contentTemplate.FindName("dragableGizmo", contentPresenter);
			//	if (dragableGizmo != null)
			//	{

			//	}
			//}
		}

		protected override void OnPreviewDragEnter(DragEventArgs e)
		{

			base.OnPreviewDragEnter(e);
		}

		#endregion drag & drop
	}
}
