using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace BL
//{
namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public double BatteryStatus { get; set; }//האם זה אכן דאבל או אינט
        public DroneStatuses DroneStatus { get; set; }
        public ParcelInDelivery ParcelInDelivery { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
//}
