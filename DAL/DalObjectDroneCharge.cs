﻿using System;
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

        public bool CheckDroneCharge(int dId, int sId)
        {
            return DataSource.droneCharges.Any(d => ((d.DroneId == dId)&&(d.StationId==sId)));
        }

        public void DroneChargesDelete(DroneCharge dc)
        {
            int count = DataSource.droneCharges.RemoveAll(d => ((d.DroneId == dc.DroneId) && (d.StationId == dc.DroneId)));
            if (count == 0)
                throw new DAL.MissingIdException(dc.DroneId, "DroneCharge");
        }

        public DroneCharge GetDroneCharge(int dId, int sId)
        {
            if (!CheckDroneCharge(dId, sId))
                throw new DAL.MissingIdException(dId, "DroneCharge");
            return DataSource.droneCharges.Find(c => ((c.DroneId == dId) && (c.StationId == sId)));
        }

       public int CountDroneCharge()
        {
            return DataSource.droneCharges.Count();
        }

    }
}
