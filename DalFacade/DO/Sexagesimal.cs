using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public  class Sexagesimal
    {
        public string ConvertToSexagesimal(double value)
        {
            int degree=(int)value;
            value = value - degree;
            int minute = (int)(value * 60);
            double second =(value * 60 - minute)*60;
            string str= Convert.ToString(degree)+'^'+ Convert.ToString(minute) + "'"+ Convert.ToString(second)+"''";
            return str;
        }

        public string ConvertLatitude(double value)
        {
            return ConvertToSexagesimal(value) + 'E';
        }
        public string ConvertLongitude(double value)
        {
            return ConvertToSexagesimal(value) + 'S';
        }
    }
}
