using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    class ConvertLatitude : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
            double val = (double)value;
            int degree = (int)val;
            val = val - degree;
            int minute = (int)(val * 60);
            double second = (val * 60 - minute) * 60;
            string str = System.Convert.ToString(degree) + " ^ " + System.Convert.ToString(minute) + " ' " + System.String.Format("{0:0.00}", second) + " '' ";
            return str + 'E';
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
    }
}
