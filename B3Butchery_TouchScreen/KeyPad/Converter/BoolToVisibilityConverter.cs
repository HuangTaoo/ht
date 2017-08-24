using System;
using System.Globalization;
using System.Windows.Data;

namespace KeyPad.Converter
{
  class BoolToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((bool?)value == true)
        return System.Windows.Visibility.Visible;
      return System.Windows.Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}