using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BlApi;
using BO;

namespace PL
{
   public class ConvertToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Drone d = new Drone();
            DroneToList dt = new DroneToList();
            double battery;
            if (value.GetType() == d.GetType())
            {
                d = (Drone)value;
               battery = (double)d.BatteryStatus;
            }
            else if(value.GetType() == dt.GetType())
            {
                dt = (DroneToList)value;
                battery = (double)dt.BatteryStatus;
            }
            else
                battery= (double)value;
            if (battery < 20)
                return Brushes.Red;
            else if (battery < 40)
                return Brushes.Orange;
            else if (battery < 60)
                return Brushes.Yellow;
            else
                return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}