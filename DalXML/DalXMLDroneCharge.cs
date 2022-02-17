using DaLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    partial class DalXML:IDAL
    {
        /// <summary>
        /// add dronecharge function
        /// </summary>
        /// <param name="dc"> the drone charge</param>
        public void DroneChargeAddition(DroneCharge dc)
        {
            if (CheckDroneCharge(dc.DroneId))
                throw new DuplicateIdException(dc.DroneId, "Drone Charge");
            List<DroneCharge> listDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            listDroneCharges.Add(dc);
            XMLTools.SaveListToXMLSerializer(listDroneCharges, DroneChargesPath);
        }

        /// <summary>
        /// function that checks if dronecharge exists
        /// </summary>
        /// <param name="dId">id of drone charge</param>
        /// <returns>true if it exists otherwise false</returns>
        public bool CheckDroneCharge(int dId)
        {
            List<DroneCharge> listDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            return listDroneCharges.Any(d => d.DroneId == dId);
        }

        /// <summary>
        /// delete dronecharge function
        /// </summary>
        /// <param name="dc">the dronecharge for delete</param>
        public void DroneChargesDelete(DroneCharge dc)
        {
            List<DroneCharge> listDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            int count = listDroneCharges.RemoveAll(d => ((d.DroneId == dc.DroneId) && (d.StationId == dc.StationId)));
            if (count == 0)
                throw new MissingIdException(dc.DroneId, "DroneCharge");
            XMLTools.SaveListToXMLSerializer(listDroneCharges, DroneChargesPath);

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
            List<DroneCharge> listDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            return listDroneCharges.Find(c => c.DroneId == dId);
        }

        /// <summary>
        /// function that returns a list of DroneCharges in specific station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>all the DroneCharges im the station</returns>
        public IEnumerable<DroneCharge> GetDroneChargeInStation(int id)
        {
            List<DroneCharge> listDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            return listDroneCharges.Where(dc => dc.StationId == id);
        }
        public IEnumerable<DroneCharge> AllDroneCharges()
        {
            List<DroneCharge> listDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            return from dr in listDrones
                   select dr;
        }
        public DroneCharge GetDroneChargeByPredicate(Predicate<DroneCharge> predicate)
        {
            List<DroneCharge> listDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargesPath);
            return (from dr in listDrones
                    where predicate(dr)
                    select dr).FirstOrDefault();
        }

    }
}
