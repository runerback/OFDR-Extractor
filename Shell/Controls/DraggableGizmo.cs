using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Extractor.Shell.Controls
{
	public class DraggableGizmo : ToggleButton
	{
		public DraggableGizmo()
		{
			this.SetResourceReference(ToggleButton.StyleProperty, typeof(ToggleButton));
			this.AllowDrop = true;
		}

		/// <summary>
		/// prevent IsChecked state changing by click
		/// </summary>
		protected override void OnToggle() { }

		protected override void OnContentChanged(object oldContent, object newContent)
		{
			if (newContent != null)
			{
				var element = newContent as FrameworkElement;
				if (element != null)
				{
					element.Focusable = false;
				}
			}
		}

		protected override void OnPreviewDragEnter(System.Windows.DragEventArgs e)
		{
			var dataObj = Data.DragDropDataObject.Parse(e.Data);
			if (dataObj.Source != this) //not self dragdrop
			{
				var sourceFileData = dataObj.Data as Data.FileDataBase;
				if (sourceFileData != null)
				{
					if (sourceFileData.CanMoveTo(this.DataContext as Data.FileDataBase))
					{
						e.Effects = DragDropEffects.Move;
						this.canDrop = true;
						return;
					}
				}
			}
			e.Effects = DragDropEffects.None;
			e.Handled = true;
			this.canDrop = false;
		}

		protected override void OnPreviewDragOver(DragEventArgs e)
		{
			if (this.canDrop)
			{
				e.Effects = DragDropEffects.Move;
			}
			else
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
			}
		}

		protected override void OnDrop(System.Windows.DragEventArgs e)
		{
			var dataObj = Data.DragDropDataObject.Parse(e.Data);
			var sourceFileData = dataObj.Data as Data.FileDataBase;
			if (sourceFileData != null)
			{
				sourceFileData.MoveTo(this.DataContext as Data.FileDataBase);
			}

			base.OnDrop(e);
		}



		private bool isDragging;
		private bool canDrop;
		private Point lastPressedLocation;

		protected override void OnPreviewMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				if (!this.isDragging)
				{
					Point newLocation = e.GetPosition(this);
					//if (this.lastPressedLocation != newLocation)
					//comparison did not work
					Vector offset = this.lastPressedLocation - newLocation;
					if (offset.LengthSquared > 18)
					{
						this.lastPressedLocation = newLocation;
						this.isDragging = true;

						if (this.DataContext != null)
						{
							var data = Data.DragDropDataObject.Create(
								this,
								this.DataContext);
							DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
							e.Handled = true;
						}
					}
					else
					{
						this.isDragging = false;
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
