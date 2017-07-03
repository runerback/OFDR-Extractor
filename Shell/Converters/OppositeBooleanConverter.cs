using System;

namespace Extractor.Shell.Converters
{
	public class OppositeBooleanConverter : System.Windows.Data.IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null && value is bool)
			{
				return !(bool)value;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null && value is bool)
			{
				return !(bool)value;
			}
			return false;
		}
	}
}
