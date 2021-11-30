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
        public void DroneChargeAddition(DroneCharge dc)
        {
            if (!CheckDroneCharge(dc.DroneId,dc.StationId))
                throw new DAL.DuplicateIdException(dc.DroneId, "Drone Charge");
            DataSource.droneCharges.Add(dc);
        }
        public bool CheckDroneCharge(int dId, int sId)

        public bool CheckDroneCharge(int dId)
        {
            return DataSource.droneCharges.Any(d => d.DroneId == dId);
        }

        public void DroneChargesDelete(DroneCharge dc)
        {
            int count = DataSource.droneCharges.RemoveAll(d => ((d.DroneId == dc.DroneId) && (d.StationId == dc.DroneId)));
            if (count == 0)
                throw new DAL.MissingIdException(dc.DroneId, "DroneCharge");
        }

        public DroneCharge GetDroneCharge(int dId)
        {
            if (!CheckDroneCharge(dId))
                throw new DAL.MissingIdException(dId, "DroneCharge");
            return DataSource.droneCharges.Find(c => c.DroneId == dId);
        }

        //function that returns a list of DroneCharges in specific station
        public IEnumerable<DroneCharge> CountDroneCharge(int id)
        {
            return DataSource.droneCharges.Where(dc => dc.StationId == id);
        }

      
    }
}
