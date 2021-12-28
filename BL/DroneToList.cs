using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

//namespace BL
//{
    namespace BO
    {
       public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public Location Location { get; set; }
            public int ParcelNumber { get; set; }  //number of parcel in delivery
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
