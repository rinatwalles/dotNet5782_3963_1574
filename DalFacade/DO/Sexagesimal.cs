using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public static class Sexagesimal
    {

        public static string ConvertToSexagesimal(double value)
        {
            int degree=(int)value;
            value = value - degree;
            int minute = (int)(value * 60);
            double second =(value * 60 - minute)*60;
            string str= Convert.ToString(degree)+'^'+ Convert.ToString(minute) + "'"+ Convert.ToString(second)+"''";
            return str;
        }
    }
}
