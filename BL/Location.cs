using System;
using IDAL.DO;

namespace BL
{
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
}
