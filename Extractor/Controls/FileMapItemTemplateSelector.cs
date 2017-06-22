using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Extractor.Controls
{
	public class FileMapItemTemplateSelector : DataTemplateSelector
	{
		public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;
			if (element != null && item != null)
			{
				if (item is Models.FileData)
				{
					return element.FindResource("file_template") as DataTemplate;
				}
				else if (item is Models.FolderData)
				{
					return element.FindResource("folder_template") as DataTemplate;
				}
			}

			return null;
		}
	}
}
