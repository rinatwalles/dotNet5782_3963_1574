using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject : DAL.IDAL.IDAL
    {
      public  bool CheckDroneCharge(int id)
        {
          //  return DataSource.droneCharges.Any(c => c.Id == id);
        }

        public void DroneChargesDelete(DroneCharge d)
        {
            DataSource.droneCharges.Remove(d);
        }
    }
}
