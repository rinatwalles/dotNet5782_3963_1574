using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

//namespace BL
//{
    namespace IBL.BO
    {
        class Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public WeightCategories weight { get; set; }
            public int batteryStatus { get; set; }
            public DroneStatuses droneStatus { get; set; }
            public ParcelInDelivery parcelInDelivery { get; set; }
            public Location location { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
