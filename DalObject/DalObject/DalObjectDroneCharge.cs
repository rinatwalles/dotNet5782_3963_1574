using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaLApi;

namespace Dal
{
     partial class DalObject : IDAL
    {
        /// <summary>
        /// add dronecharge function
        /// </summary>
        /// <param name="dc"> the drone charge</param>
        public void DroneChargeAddition(DroneCharge dc)
        {
            if (CheckDroneCharge(dc.DroneId))
                throw new DuplicateIdException(dc.DroneId, "Drone Charge");
            DataSource.droneCharges.Add(dc);
        }

        /// <summary>
        /// function that checks if dronecharge exists
        /// </summary>
        /// <param name="dId">id of drone charge</param>
        /// <returns>true if it exists otherwise false</returns>
        public bool CheckDroneCharge(int dId)
        {
            return DataSource.droneCharges.Any(d => d.DroneId == dId);
        }

        /// <summary>
        /// delete dronecharge function
        /// </summary>
        /// <param name="dc">the dronecharge for delete</param>
        public void DroneChargesDelete(DroneCharge dc)
        {
            int count = DataSource.droneCharges.RemoveAll(d => ((d.DroneId == dc.DroneId) && (d.StationId == dc.StationId)));
            if (count == 0)
                throw new MissingIdException(dc.DroneId, "DroneCharge");
        }

        /// <summary>
        /// function that gets id of a DroneCharges and returns the DroneCharges
        /// </summary>
        /// <param name="dId">id of a DroneCharges</param>
        /// <returns>the DroneCharges</returns>
        public DroneCharge GetDroneCharge(int dId)
        {
            if (!CheckDroneCharge(dId))
                throw new MissingIdException(dId, "DroneCharge");
            return DataSource.droneCharges.Find(c => c.DroneId == dId);
        }

        /// <summary>
        /// function that returns a list of DroneCharges in specific station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>all the DroneCharges im the station</returns>
        public IEnumerable<DroneCharge> GetDroneChargeInStation(int id)
        {
            return DataSource.droneCharges.Where(dc => dc.StationId == id);
        }
    }
}
