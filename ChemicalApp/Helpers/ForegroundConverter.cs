using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ChemicalApp.Helpers
{
    class ForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null || values[2] == null) return DependencyProperty.UnsetValue;
            if (!(values[0] is decimal Min)) return DependencyProperty.UnsetValue;
            if (!(values[1] is decimal Max)) return DependencyProperty.UnsetValue;
            if (!(values[2] is decimal Predicted)) return DependencyProperty.UnsetValue;
            if (Predicted > Max)
                return Brushes.Black;
            else
                return Brushes.DimGray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return new object[] { value };
        }

    }
}
