using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StackApp.ViewModel.Converters
{
    public class OffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // The first value is our LinkPoint.X, Y, and the second value is Canvas.Left, Top
            double d = (double)values[1];
            if (double.IsNaN(d))
            {
                d = 0.0;
            }
            return d + (double)values[0];
        }

        public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
