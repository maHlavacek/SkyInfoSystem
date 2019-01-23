using System;
using System.Globalization;
using System.Windows.Data;

namespace SkiInfoSystem.UI.Wpf
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number;
            int.TryParse(value.ToString(), out number);
            return number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)((double)value);
            return number.ToString();
        }
    }
}
