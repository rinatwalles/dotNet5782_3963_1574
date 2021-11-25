using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
//namespace BL
//{
    namespace IBL.BO
    {
        public class BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location SLocation { get; set; }
            public int AvilableChargeSlotsNumber { get; set; }
            public List<DroneCharging> DroneCharging { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
