using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Extractor.Shell.Controls
{
	public class StatusPresenter : StackPanel
	{
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			this.Orientation = System.Windows.Controls.Orientation.Vertical;

			ProgressBar progressBar = new ProgressBar() { Maximum = 100.0 };

			BindingBase pgBarValueBinding = new Binding()
			{
				Path = new PropertyPath("CurrentProgress"),
				Mode = BindingMode.OneWay
			};
			BindingOperations.SetBinding(progressBar, ProgressBar.ValueProperty, pgBarValueBinding);

			BindingBase pgBarVisibilityBinding = new Binding()
			{
				Path = new PropertyPath("ProgressBarVisibility"),
				Mode = BindingMode.OneWay
			};
			BindingOperations.SetBinding(progressBar, ProgressBar.VisibilityProperty, pgBarVisibilityBinding);


			TextBlock statusBar = new TextBlock() { Margin = new Thickness(0) };

			BindingBase statusBarValueBinding = new Binding()
			{
				Path = new PropertyPath("Status"),
				Mode = BindingMode.OneWay
			};
			BindingOperations.SetBinding(statusBar, TextBlock.TextProperty, statusBarValueBinding);

			this.Children.Add(statusBar);
			this.Children.Add(progressBar);
		}
	}
}
