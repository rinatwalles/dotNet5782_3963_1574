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


       //public void UpdateStation(int id, string name = "", string allChargingPositions = "")
        private IDAL.DO.Station minStationDistance(IBL.BO.Drone boDrone)
        {
            IDAL.DO.Station minStation = new IDAL.DO.Station();
            try
            { 
                double minDistance = 1000000;
                Location sLocation = new Location { };
                foreach (IDAL.DO.Station item in idal.GetStationByPredicate(st => st.AvailableChargeSlots > 0))  //searching the closest station to the sender
                {
                    sLocation.Latitude = item.Latitude;
                    sLocation.Longitude = item.Longitude;
                    double dist = getDistance(sLocation, boDrone.Location);
                    if (dist * array[1 + (int)boDrone.Weight] <= boDrone.BatteryStatus)
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            minStation = item;
                        }
                }
                if (minDistance == 1000000)
                    throw new notEnoughFuelInDrone(boDrone.Id, "Drone");
                //IDAL.DO.Station doStation = new IDAL.DO.Station();
                //doStation = idal.GetStation(id);
                ////if (name != "")
                ////    doStation.Name = name;
                ////if (allChargingPositions != "")
                ////    doStation.AvailableChargeSlots = Int32.Parse(allChargingPositions) - idal.CountDroneCharge(id).Count();
                //idal.StationUpdate(doStation);
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
                //return
            }
            return minStation;
        }
        
        public void droneToCharge (int id)
        {
            try 
            {
                IBL.BO.Drone boDrone = GetDrone(id);
                if (boDrone.DroneStatus != DroneStatuses.Available)//אם הרחפן לא פנוי פשוט לצאת??
                    return;
                double[] array = idal.AskingElectricityUse();//למחוק אחרי פוש !!1
                IDAL.DO.Station minStation = minStationDistance(boDrone);
                Location sLocation = new Location { };
                DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                ListBLDrones.RemoveAll(d => d.Id == id);
                sLocation.Latitude = minStation.Latitude;
                sLocation.Longitude = minStation.Longitude;
                double minDistance = getDistance(sLocation, boDrone.Location);
                dtl.BatteryStatus -= array[1 + (int)boDrone.Weight] * minDistance;//לבדוק אם ניגש לרחפן ברשימה 
                dtl.Location = sLocation;
                dtl.DroneStatus = DroneStatuses.Maintenance;
                ListBLDrones.Add(dtl);
                minStation.AvailableChargeSlots--;
                idal.StationUpdate(minStation);
                IDAL.DO.DroneCharge dc = new IDAL.DO.DroneCharge();
                dc.StationId = minStation.Id;
                dc.DroneId = minStation.Id;
                idal.DroneChargeAddition(dc);
            }
           catch(DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }
        private Location minStationDistance(Location location)
        {
            IDAL.DO.Station minStation = new IDAL.DO.Station();
            double minDistance = 1000000;
            Location sLocation = new Location { };
            foreach (IDAL.DO.Station item in idal.GetStationByPredicate(st => st.AvailableChargeSlots > 0))  //searching the closest station to the sender
            {
                sLocation.Latitude = item.Latitude;
                sLocation.Longitude = item.Longitude;
                double dist = getDistance(sLocation, location);
                if (dist < minDistance)
                {
                    minStation = item;
                }
            }
            return new Location
            {
                Latitude = minStation.Latitude,
                Longitude = minStation.Longitude
            };
        }
        public void joinParcelToDrone (int id)
        {

            IBL.BO.Drone boDrone = GetDrone(id);
            if (boDrone.DroneStatus != DroneStatuses.Available)//אם הרחפן לא פנוי פשוט לצאת??
                return;
            Priorities maxPriority = Priorities.Regular;
            double minDistance = 10000000;//מקווה שזה לגיטמי ככה להגדיר מינימום מה גם שאני לא יודעת סדרי גודל 
            double droneToSender, senderToTarget, targetToStation;
            IDAL.DO.Parcel chosenOne = new IDAL.DO.Parcel();
            
            foreach (IDAL.DO.Parcel item in idal.GetParcelByPredicate(par => (int)par.Weight <= (int)boDrone.Weight))
            {
                droneToSender = getDistance(GetCustomer(item.SenderId).Location, boDrone.Location);
                senderToTarget = getDistance(GetCustomer(item.SenderId).Location, GetCustomer(item.TargetId).Location);
                targetToStation= getDistance(minStationDistance(GetCustomer(item.TargetId).Location), GetCustomer(item.TargetId).Location));
                if (maxPriority < (Priorities)item.Priority)
                    if (droneToSender < minDistance)
                        if (array[1 + (int)boDrone.Weight] * (droneToSender + targetToStation + senderToTarget) <= boDrone.BatteryStatus)
                            chosenOne = item;
            }
            DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
            dtl.DroneStatus = DroneStatuses.Maintenance;
            ListBLDrones.RemoveAll(d => d.Id == id);
            ListBLDrones.Add(dtl);
            chosenOne.DroneId = id;
            chosenOne.RequestedTime = DateTime.Now;
            idal.ParcelUpdate(chosenOne);
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

        public void ReleaseDroneFromCharge(int droneId, TimeSpan t)
        {
            IBL.BO.DroneToList dron = new IBL.BO.DroneToList();
            dron = ListBLDrones.Find(d => d.Id == droneId);
            if (dron.DroneStatus != IBL.BO.DroneStatuses.Maintenance)   //cant be released
                throw new DeliveryProblems(droneId, "Drone not in ,aintance status");
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
            DateTime t = DateTime.MinValue;
            IBL.BO.DroneToList dron = new IBL.BO.DroneToList();
            dron = ListBLDrones.Find(d => d.Id == droneId);
            IDAL.DO.Parcel parc = idal.GetParcel(dron.ParcelNumber);
            if(parc.PickedUpTime!=t)
                throw new DeliveryProblems(droneId, "Drone already picked up");
            Location locat = new Location();
            locat=dron.Location;              //current location of drone

            IDAL.DO.Customer sender = idal.GetCustomer(parc.SenderId);    //the location of the sender
            dron.BatteryStatus=idal.DistanceCalculate(sender.Longitude, sender.Latitude, locat.Longitude, locat.Latitude) * array[1 + (int)dron.Weight]; //distance between the sender and the current location
            locat.Latitude = sender.Latitude;
            locat.Longitude = sender.Longitude;
            dron.Location = locat;    //updating the place to the sender place

            parc.PickedUpTime = DateTime.Now;
            idal.ParcelUpdate(parc);
        }

    }
}
