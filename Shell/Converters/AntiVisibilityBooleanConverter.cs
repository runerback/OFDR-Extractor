using System;

namespace Extractor.Shell.Converters
{
	public class AntiVisibilityBooleanConvert : System.Windows.Data.IValueConverter
	{
		/// <summary>
		/// Boolean to Visibility
		/// </summary>
		/// <param name="value">bool</param>
		/// <param name="targetType"></param>
		/// <param name="parameter">keep null</param>
		/// <param name="culture"></param>
		/// <returns>Visibility</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null && value is bool)
			{
				if (!(bool)value)
					return System.Windows.Visibility.Visible;
				else
					return System.Windows.Visibility.Collapsed;
			}
			return System.Windows.Visibility.Hidden;
		}

		/// <summary>
		/// Visibility to Boolean
		/// </summary>
		/// <param name="value">Visibility</param>
		/// <param name="targetType"></param>
		/// <param name="parameter">keep null</param>
		/// <param name="culture"></param>
		/// <returns>Boolean</returns>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null)
			{
				if (value is System.Windows.Visibility)
				{
					if ((System.Windows.Visibility)value ==
						System.Windows.Visibility.Collapsed)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else if (value is string)
				{
					if (value as string == "Collapsed")
					{
						return true;
					}
					else if (value as string == "Hidden")
					{
						return true;
					}
					else if (value as string == "Visible")
					{
						return false;
					}
				}
			}
			return false;
		}
	}
}
