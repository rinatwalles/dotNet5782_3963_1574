using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        
        public void UpdateDrone(int id, string model)
        {//מסתמכים שאם הוא נמצר בDAL כבר הוספנו אותו לBL. לא להסתמך על זה?
            try
            {//שינוי הרחפן בDAL
                IDAL.DO.Drone doDrone = new IDAL.DO.Drone();
                doDrone = idal.GetDrone(id);   //הצבה בין שדות?
                //IDAL.DO.Drone doDrone = idal.GetDrone(id);     השורה הזאת יכולה להחליף את 145, 146? כי יכול להיות שמאחורי הקלעים יש new
                doDrone.Model = model;
                idal.DroneUpdate(doDrone);

                //שינוי רחפן ברישמת רחפנים BL
                DroneToList dl = ListBLDrones.Find(d => d.Id == id);        //יצרנו העתק
                int count = ListBLDrones.RemoveAll(dr => dr.Id == id); //מוחקים את הישן
                dl.Model = model;  // changing the model name
                ListBLDrones.Add(dl); //adding the new one
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            try
            {
                IDAL.DO.Customer doCustomer = new IDAL.DO.Customer();
                doCustomer = idal.GetCustomer(id);
                if (name != "")                  //בדיקה אם הוא הכניב ערכים או ENTER
                    doCustomer.Name = name;
                if (phone != "")
                    doCustomer.Phone = phone;
                idal.CustomerUpdate(doCustomer);
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }


       public void UpdateStation(int id, string name = "", string allChargingPositions = "")
        {
            try
            {
                IDAL.DO.Station doStation = new IDAL.DO.Station();
                doStation = idal.GetStation(id);
                if (name != "")
                    doStation.Name = name;
                if (allChargingPositions != "")
                    doStation.AvailableChargeSlots = Int32.Parse(allChargingPositions) - idal.CountDroneCharge(id).Count();
                idal.StationUpdate(doStation);
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void droneToCharge (int id)
        {
            try 
            {
                IBL.BO.Drone boDrone = GetDrone(id);
                if (boDrone.DroneStatus != DroneStatuses.Available)//אם הרחפן לא פנוי פשוט לצאת??
                    return;
                Location sLocation = new Location { };
                double calculate;
                foreach (IDAL.DO.Station item in idal.GetStationByPredicate(st=>st.AvailableChargeSlots>0))
                {
                    double dist = idal.DistanceCalculate(stat.Longitude, stat.Latitude, locat.Longitude, locat.Latitude);
                    if (dist < minDistance)
                    {
                        newlocat.Latitude = stat.Latitude;
                        newlocat.Longitude = stat.Longitude;
                        minDistance = dist;
                    }
                }

                //    
                //
                //calculate += idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];

            }
           catch(DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }

        public void ReleaseDroneFromCharge(int droneId, TimeSpan t)
        {
            IBL.BO.DroneToList dron = new IBL.BO.DroneToList();
            dron = ListBLDrones.Find(d => d.Id == droneId);
            if (dron.DroneStatus != IBL.BO.DroneStatuses.Maintenance)   //cant be released
                throw new DroneNotMaintance(droneId, "Drone");
            dron.BatteryStatus = array[1 + (int)dron.Weight]*(t.TotalSeconds/60);  //time of charging
            dron.DroneStatus = IBL.BO.DroneStatuses.Available;

            //adding one available station
            IDAL.DO.DroneCharge dc = idal.GetDroneCharge(dron.Id);
            IDAL.DO.Station stat = idal.GetStation(dc.StationId);
            stat.AvailableChargeSlots++;
            idal.StationUpdate(stat);

            //deleteing the drone charge from the list
            idal.DroneChargesDelete(dc);
        }

        public void PickedUpParcelByDrone(int droneId)
        {

        }

    }
}
