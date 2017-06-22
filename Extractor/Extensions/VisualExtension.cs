using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace System.Windows.Controls
{
	public static class VisualExtension
	{
		public static TElement FindVisualChild<TElement>(this DependencyObject obj) 
			where TElement : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is TElement)
					return (TElement)child;
				else
				{
					TElement childOfChild = FindVisualChild<TElement>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}
	}
}
