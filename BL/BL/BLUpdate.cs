
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;

namespace BL
{
     partial class BL : IBL
    {
        /// <summary>
        /// function that gets a drone and update it
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <param name="model">drone's model</param>
        public void UpdateDrone(int id, string model)
        {//מסתמכים שאם הוא נמצר בDAL כבר הוספנו אותו לBL. לא להסתמך על זה?
            try
            {//changing drone in DAL
                DO.Drone doDrone = new DO.Drone();
                doDrone = idal.GetDrone(id);  
                doDrone.Model = model;
                idal.DroneUpdate(doDrone);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

            //changing the droe in BL list
            DroneToList dl = ListBLDrones.Find(d => d.Id == id);        //creating copy
            int count = ListBLDrones.RemoveAll(dr => dr.Id == id); //deleting the old one
            dl.Model = model;  // changing the model name
            ListBLDrones.Add(dl); //adding the new one
        }

        /// <summary>
        /// function that gets a customer and update it in the list
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <param name="name">customer's name</param>
        /// <param name="phone">customer's phone</param>
        public void UpdateCustomer(int id, string name, string phone)
        {
            try
            {
                DO.Customer doCustomer = new DO.Customer();
                doCustomer = idal.GetCustomer(id);
                if (name != "")                  //checking if inserts a value or pressed ENTER
                    doCustomer.Name = name;
                if (phone != "")
                    doCustomer.Phone = phone;
                idal.CustomerUpdate(doCustomer);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// function that returns a station closest to the drone location
        /// </summary>
        /// <param name="boDrone">BL drone</param>
        /// <returns>DAL station</returns>
        private DO.Station minStationDistance(BO.Drone boDrone)
        {
            DO.Station minStation = new DO.Station();
            try
            { 
                double minDistance = 1000000;
                Location sLocation = new Location { };
                foreach (DO.Station item in idal.GetStationByPredicate(st => st.AvailableChargeSlots > 0))  //searching the closest station to the sender
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
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return minStation;
        }
        
        /// <summary>
        /// function that put a drone into charging
        /// </summary>
        /// <param name="id">id of drone</param>
        public void droneToCharge (int id)
        {
            try 
            {
                BO.Drone boDrone = GetDrone(id);
                if (boDrone.DroneStatus != DroneStatuses.Available)
                    throw new DeliveryProblems(boDrone.Id, "The drone doesn't available");

                DO.Station minStation = minStationDistance(boDrone);
                Location sLocation = new Location { };
                DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                ListBLDrones.RemoveAll(d => d.Id == id);
                sLocation.Latitude = minStation.Latitude;
                sLocation.Longitude = minStation.Longitude;
                double minDistance = getDistance(sLocation, boDrone.Location);
                dtl.BatteryStatus -= array[1 + (int)boDrone.Weight] * minDistance;//לבדוק אם ניגש לרחפן ברשימה 
                dtl.Location = sLocation;
                dtl.DroneStatus = DroneStatuses.Maintenance;
                //updating the BL list
                ListBLDrones.Add(dtl);
                minStation.AvailableChargeSlots--;
                idal.StationUpdate(minStation);
                DO.DroneCharge dc = new DO.DroneCharge();
                dc.StationId = minStation.Id;
                dc.DroneId = id;
                if(!idal.CheckDroneCharge(boDrone.Id))
                    idal.DroneChargeAddition(dc);
            }
           catch(MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }

        /// <summary>
        /// function that gets location and returns the station with the min distance
        /// </summary>
        /// <param name="location">the location</param>
        /// <returns>closest station</returns>
        private Location minStationDistance(Location location)
        {
            DO.Station minStation = new DO.Station();
            double minDistance = 1000000;
            Location sLocation = new Location { };
            foreach (DO.Station item in idal.GetStationByPredicate(st => st.AvailableChargeSlots > 0))  //searching the closest station to the sender
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

        /// <summary>
        /// function that connect between drone and parcel
        /// </summary>
        /// <param name="id">id of drone</param>
        public void joinParcelToDrone (int id)
        {
            try
            {
                BO.Drone boDrone = GetDrone(id);
                if (boDrone.DroneStatus != DroneStatuses.Available)
                    throw new DeliveryProblems(boDrone.Id, "The drone doesn't available");

                Priorities maxPriority = Priorities.Regular;
                double minDistance = 10000000;//מקווה שזה לגיטמי ככה להגדיר מינימום מה גם שאני לא יודעת סדרי גודל 
                double droneToSender, senderToTarget, targetToStation;
                DO.Parcel chosenOne = new DO.Parcel();
                int counter = 0;

                foreach (DO.Parcel item in idal.GetParcelByPredicate(par => (int)par.Weight <= (int)boDrone.Weight))
                {
                    droneToSender = getDistance(GetCustomer(item.SenderId).Location, boDrone.Location);
                    senderToTarget = getDistance(GetCustomer(item.TargetId).Location, GetCustomer(item.SenderId).Location);
                    targetToStation = getDistance(minStationDistance(GetCustomer(item.TargetId).Location), GetCustomer(item.TargetId).Location);
                    if(item.ScheduledTime== DateTime.MinValue)
                        if (maxPriority <= (Priorities)item.Priority)
                            if (droneToSender < minDistance)
                                if (array[1 + (int)boDrone.Weight] * (droneToSender + targetToStation + senderToTarget) <= boDrone.BatteryStatus)
                                {
                                    maxPriority = (Priorities)item.Priority;
                                    chosenOne = item;
                                    counter = 1;
                                    break;
                                }
                }
                if (counter != 0)
                {
                    DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                    dtl.DroneStatus = DroneStatuses.Delivery;
                    dtl.ParcelNumber = chosenOne.Id;
                    //updating the BL list
                    ListBLDrones.RemoveAll(d => d.Id == id);
                    ListBLDrones.Add(dtl);
                    chosenOne.DroneId = id;
                    chosenOne.ScheduledTime = DateTime.Now;
                    idal.ParcelUpdate(chosenOne);
                }
                else
                    throw new DeliveryProblems(id, "can't join parcel to drone");
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// function that gets a station and update its feilds
        /// </summary>
        /// <param name="id">the id of station</param>
        /// <param name="name">the station's name</param>
        /// <param name="allChargingPositions">num of ChargingPositions in string </param>
        public void UpdateStation(int id, string name , string allChargingPositions)
        {
            try
            {
                DO.Station doStation = new DO.Station();
                doStation = idal.GetStation(id);
                if (name != "")
                    doStation.Name = name;
                if (allChargingPositions != "")
                    doStation.AvailableChargeSlots = Int32.Parse(allChargingPositions) - idal.GetDroneChargeInStation(id).Count();
                idal.StationUpdate(doStation);
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// function that releases the drone from charging
        /// </summary>
        /// <param name="droneId">id of a drone</param>
        /// <param name="t">time that the drone charged</param>
        public void ReleaseDroneFromCharge(int droneId, TimeSpan t)
        {
            try
            {
                BO.DroneToList dron = new BO.DroneToList();
                dron = ListBLDrones.Find(d => d.Id == droneId);
                if (dron.DroneStatus != BO.DroneStatuses.Maintenance)   //cant be released
                    throw new DeliveryProblems(droneId, "Drone not in maintance status");
                dron.BatteryStatus += array[4] * (t.TotalMinutes / 60);  //time of charging
                if (dron.BatteryStatus > 100)
                    dron.BatteryStatus = 100;
                dron.DroneStatus = BO.DroneStatuses.Available;
                //updating the BL list
                ListBLDrones.RemoveAll(d => d.Id == droneId);
                ListBLDrones.Add(dron);

                //adding one available station
                DO.DroneCharge dc = idal.GetDroneCharge(dron.Id);
                DO.Station stat = idal.GetStation(dc.StationId);
                stat.AvailableChargeSlots++;
                idal.StationUpdate(stat);

                //deleteing the drone charge from the list
                idal.DroneChargesDelete(dc);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// function that update the drone to pickם up the parcel from the customer
        /// </summary>
        /// <param name="droneId">id of a drone</param>
        public void PickedUpParcelByDrone(int droneId)
        {
            try
            {
                DateTime t = DateTime.MinValue;
                BO.DroneToList dron = new BO.DroneToList();
                dron = ListBLDrones.Find(d => d.Id == droneId);
                DO.Parcel parc = idal.GetParcel(dron.ParcelNumber);
                if (parc.PickedUpTime != t)
                    throw new DeliveryProblems(droneId, "Drone already picked up");
                Location locat = new Location();
                locat = dron.Location;              //current location of drone

                DO.Customer sender = idal.GetCustomer(parc.SenderId);    //the location of the sender

                dron.BatteryStatus -= idal.DistanceCalculate(sender.Longitude, sender.Latitude, locat.Longitude, locat.Latitude) * array[1 + (int)dron.Weight]; //distance between the sender and the current location
                locat.Latitude = sender.Latitude;
                locat.Longitude = sender.Longitude;
                dron.Location = locat;    //updating the place to the sender place
                dron.DroneStatus = DroneStatuses.Delivery;

                parc.PickedUpTime = DateTime.Now;
                idal.ParcelUpdate(parc);
                //updating the BL list
                ListBLDrones.RemoveAll(d => d.Id == droneId);
                ListBLDrones.Add(dron);

            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            catch(DeliveryProblems ex)
            {
                throw new DeliveryProblems(droneId, "Drone already picked up");
            }
        }

        /// <summary>
        /// function that update the parcel supllied to the customer
        /// </summary>
        /// <param name="id">id of drone</param>
        public void supplyParceByDrone(int id)
        {
            try
            {
                BO.Drone boDrone = GetDrone(id);
                DO.Parcel doParcel = idal.GetOneParcelByPredicate(p => p.DroneId == id);
                //IBL.BO.Parcel boParcel = getParcel(doParcel.Id);
                if (getParcelState(doParcel.Id) == ParcelStates.Delivered)
                    throw new DeliveryProblems(id, "The drone didn't picked up the parcel or alredy delivered it");
                double senderToTarget = getDistance(GetCustomer(doParcel.TargetId).Location, GetCustomer(doParcel.SenderId).Location);
                DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                ListBLDrones.RemoveAll(d => d.Id == id);
                dtl.BatteryStatus -= array[1 + (int)boDrone.Weight] * senderToTarget;
                dtl.Location = GetCustomer(doParcel.TargetId).Location;
                dtl.DroneStatus = DroneStatuses.Available;
                doParcel.DeliveredTime = DateTime.Now;
                doParcel.DroneId = 0;
                idal.ParcelUpdate(doParcel);
                //updating the BL list
                ListBLDrones.Add(dtl);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void deleteParcel(int id)
        {
            ParcelStates ps = getParcelState(id);
            if (ps == ParcelStates.Requested || ps == ParcelStates.Creation)
                idal.ParcelDelete(id);
            throw new DeliveryProblems(id, "The parcel id delivered");
        }
        //public void autoUpdate (int drId)//לשנות שיקבל id?
        //{
        //    Drone drone = GetDrone(drId);
        //    try
        //    {
        //        switch (drone.DroneStatus)
        //        {
        //            case DroneStatuses.Available:
        //                if (drone.BatteryStatus < 40)
        //                    droneToCharge(drone.Id);
        //                else
        //                    joinParcelToDrone(drone.Id);
        //                break;
        //            case DroneStatuses.Delivery:
        //                ParcelStates ps = getParcelState(drone.ParcelInDelivery.Id);
        //                switch (ps)
        //                {
        //                    case ParcelStates.Scheduled:
        //                        PickedUpParcelByDrone(drone.Id);
        //                        break;
        //                    case ParcelStates.PickedUp:
        //                        supplyParceByDrone(drone.Id);
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                break;
        //            case DroneStatuses.Maintenance:
        //                ReleaseDroneFromCharge(drone.Id, TimeSpan.Parse("1000"));
        //                break;
        //            default:
        //                break;

        //        }
        //    }
        //    catch (DeliveryProblems ex)
        //    {
        //        droneToCharge(drone.Id);
        //    }
        //}
    }
}
