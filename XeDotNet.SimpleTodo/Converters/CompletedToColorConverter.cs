using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace XeDotNet.SimpleTodo.Converters
{
    public class CompletedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (bool) value ? Colors.LightGray : Colors.Black;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}