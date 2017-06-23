using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Extractor.Controls
{
	public class DraggableGizmo : ToggleButton
	{
		public DraggableGizmo()
		{
			this.SetResourceReference(ToggleButton.StyleProperty, typeof(ToggleButton));
			this.AllowDrop = true;
		}

		protected override void OnContentChanged(object oldContent, object newContent)
		{
			if (newContent != null)
			{
				var element = newContent as FrameworkElement;
				if (element != null)
				{
					element.AllowDrop = false;
					element.Focusable = false;
				}
			}
		}

		protected override void OnPreviewDragEnter(System.Windows.DragEventArgs e)
		{
			if (object.ReferenceEquals(e.Source, this))
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				this.isSelfDragging = true;
			}
			else
			{
				this.isSelfDragging = false;
			}
			base.OnPreviewDragEnter(e);
		}

		protected override void OnPreviewDragOver(DragEventArgs e)
		{
			if (this.isSelfDragging)
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
			}
			base.OnPreviewDragOver(e);
		}

		protected override void OnDrop(System.Windows.DragEventArgs e)
		{
			base.OnDrop(e);
		}

		private bool isDragging;
		private bool isSelfDragging;

		protected override void OnPreviewMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				if (!this.isDragging)
				{
					this.isDragging = true;

					var data = this.DataContext as Models.FileDataBase;
					if (data != null)
					{
						DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
						e.Handled = true;
					}
				}
			}
		}

		protected override void OnPreviewDragLeave(DragEventArgs e)
		{
			if (this.isDragging)
				this.isDragging = false;
		}

		protected override void OnPreviewMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.isDragging)
				this.isDragging = false;
		}
	}
}
