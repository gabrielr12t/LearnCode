using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LearnCode.Client.Converters
{
    public class SearchTextToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)values[0] == "") return Visibility.Visible;

            string searchText = ((string)values[0]).ToLower();
            object row = values[1];

            foreach (var p in row.GetType().GetProperties())
                if (System.Convert.ToString(p.GetValue(row)).ToLower().Contains(searchText))
                    return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
