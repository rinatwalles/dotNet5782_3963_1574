using System;
using IBL.BO;
using IDAL.DO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//namespace BL
//{
namespace IBL.BO
    {
        public class Location
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
