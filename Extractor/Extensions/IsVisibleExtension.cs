using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Extensions
{
	public class IsVisibleExtension : FrameworkElement
	{
		public static bool GetVisible(FrameworkElement d)
		{
			return (bool)d.GetValue(IsVisibleExtension.VisibleProperty);
		}

		public static void SetVisible(FrameworkElement d, bool value)
		{
			d.SetValue(IsVisibleExtension.VisibleProperty, value);
		}

		public static readonly DependencyProperty VisibleProperty =
			DependencyProperty.RegisterAttached(
				"Visible",
				typeof(bool),
				typeof(IsVisibleExtension),
				new PropertyMetadata(true, onIsVisiblePropertyChanged));

		private static void onIsVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != null)
			{
				bool value = (bool)e.NewValue;
				if (value)
				{
					d.SetValue(FrameworkElement.VisibilityProperty, Visibility.Visible);
				}
				else
				{
					d.SetValue(FrameworkElement.VisibilityProperty, Visibility.Collapsed);
				}
			}
		}

	}
}
