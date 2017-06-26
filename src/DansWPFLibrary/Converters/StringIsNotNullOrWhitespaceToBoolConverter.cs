using System;
using System.Globalization;
using System.Windows.Data;

namespace DansWPFLibrary.Converters
{
	public class StringIsNotNullOrWhitespaceToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !string.IsNullOrWhiteSpace(value.ToString());
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}