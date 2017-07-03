using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Extractor.Shell.Controls
{
	public class StatusPresenter : ContentControl
	{
		protected override void OnInitialized(EventArgs e)
		{
			Grid root = new Grid() { Margin = default(Thickness) };

			root.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(6) });
			root.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

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

			root.Children.Add(progressBar);
			Grid.SetRow(progressBar, 0);


			TextBlock statusBar = new TextBlock() { Margin = default(Thickness) };
			BindingBase statusBarValueBinding = new Binding()
			{
				Path = new PropertyPath("Status"),
				Mode = BindingMode.OneWay
			};
			BindingOperations.SetBinding(statusBar, TextBlock.TextProperty, statusBarValueBinding);

			root.Children.Add(statusBar);
			Grid.SetRow(statusBar, 1);

			this.Content = root;

			base.OnInitialized(e);
		}
	}
}
