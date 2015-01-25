using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nega.WpfCommon
{

    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;
            if (value != null)
            {
                result = (bool)value;
            }
            return result ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Visibility result = System.Windows.Visibility.Collapsed;
            if (value != null)
            {
                result = (System.Windows.Visibility)value;
            }
            return System.Windows.Visibility.Visible == result ? true : false;
        }
    }

}
