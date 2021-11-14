using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace IBL.BO
    {
        class BaseStation
        {
            public int id { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public int avilableChargeSlotsNumber { get; set; }
            public List<DroneCharging> droneCharging { get; set; }
        }
    }
}
