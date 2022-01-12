
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
        ///  a function that gets an id of a station returns the station
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns></returns>
        public Station GetStation(int id)
        {
            BO.Station boBaseStation = new BO.Station();
            try
            {
                DO.Station doStation = idal.GetStation(id);
                boBaseStation.Id = doStation.Id;
                boBaseStation.Name = doStation.Name;
                boBaseStation.Location = new Location
                {
                    Latitude = doStation.Latitude,
                    Longitude = doStation.Longitude
                };
                boBaseStation.AvailableChargeSlots = doStation.AvailableChargeSlots;
                boBaseStation.DroneCharging = GetDroneChargingPerStation(id);
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boBaseStation;
        }

        /// <summary>
        ///  a function that gets an id of a drone returns the drone
        /// </summary>
        /// <param name="id">id of a drone</param>
        /// <returns></returns>
        public Drone GetDrone(int id)
        {
            BO.Drone boDrone = new BO.Drone();
            try
            {
                DO.Drone doDrone = idal.GetDrone(id);
                boDrone.Id = doDrone.Id;
                boDrone.Model = doDrone.Model;
                boDrone.Weight = (BO.WeightCategories)doDrone.Weight;
                BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boDrone.BatteryStatus = dtl.BatteryStatus;
                boDrone.DroneStatus = dtl.DroneStatus;
                boDrone.Location = dtl.Location;
                //boDrone.ParcelInDelivery מה עם זה
                if (boDrone.DroneStatus == BO.DroneStatuses.Delivery)
                {
                    DO.Parcel boParcel = idal.GetOneParcelByPredicate(p => p.DroneId == id);
                    //IDAL.DO.Customer boSender = idal.getParcelByDroneId(id);
                    boDrone.ParcelInDelivery = new ParcelInDelivery
                    {
                        Id = boParcel.Id,
                        SenderName = new CustomerOfParcel
                        {
                            Id = boParcel.SenderId,//by parcel id
                            Name = idal.GetCustomer(boParcel.SenderId).Name,//by customer id who is the sender in the parcel
                        },
                        ReceiverName = new CustomerOfParcel
                        {
                            Id = boParcel.TargetId,//by parcel id
                            Name = idal.GetCustomer(boParcel.TargetId).Name,//by customer id who is the reciever in the parcel
                        },
                        Weight = (WeightCategories)boParcel.Weight,
                        Priority = (Priorities)boParcel.Priority,
                        CollectingPlace = new Location
                        {
                            Longitude = idal.GetCustomer(boParcel.SenderId).Longitude,//locationr of the sender by parcel, sender, customer name
                            Latitude = idal.GetCustomer(boParcel.SenderId).Latitude
                        },
                        DestinationPlace = new Location
                        {
                            Longitude = idal.GetCustomer(boParcel.TargetId).Longitude,//locationr of the reciever by parcel, sender, customer name
                            Latitude = idal.GetCustomer(boParcel.TargetId).Latitude
                        },
                        //TransportDistance = getDistance(boDrone.ParcelInDelivery.CollectingPlace, boDrone.ParcelInDelivery.DestinationPlace)
                    };
                    boDrone.ParcelInDelivery.TransportDistance = getDistance(boDrone.ParcelInDelivery.CollectingPlace, boDrone.ParcelInDelivery.DestinationPlace);
                    boDrone.ParcelInDelivery.ParcelState = getParcelState(id) == ParcelStates.PickedUp;//החבילה נאספה והיא בדרך
                    ParcelStates ps = getParcelState(boDrone.ParcelInDelivery.Id);
                    if (ps == ParcelStates.Requested || ps == ParcelStates.Creation)
                        boDrone.ParcelInDelivery.ParcelState = false;
                    else
                        boDrone.ParcelInDelivery.ParcelState = true;
                }
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boDrone;
        }

        /// <summary>
        /// a function that gest 2 locations and returns the distance between them
        /// </summary>
        /// <param name="l1">the first location</param>
        /// <param name="l2">the second location</param>
        /// <returns></returns>
        private double getDistance(Location l1, Location l2)
        {
            return idal.DistanceCalculate(l1.Latitude, l1.Latitude, l2.Latitude, l2.Longitude);
        }

        /// <summary>
        /// a function that gets an id of a parcel and returns its state
        /// </summary>
        /// <param name="id">id of a parcel</param>
        /// <returns></returns>
        private ParcelStates getParcelState(int id)
        {
            try
            {
                DO.Parcel doParcel = idal.GetParcel(id);
                if (doParcel.RequestedTime == DateTime.MinValue)
                    return ParcelStates.Creation;
                else if (DateTime.MinValue == doParcel.ScheduledTime)
                    return ParcelStates.Requested;
                else if ( DateTime.MinValue == doParcel.PickedUpTime)
                        return ParcelStates.Scheduled;
                else if (DateTime.MinValue == doParcel.DeliveredTime)
                        return ParcelStates.PickedUp;
                else 
                    return ParcelStates.Delivered;
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that gets an id of a customer returns the customer
        /// </summary>
        /// <param name="id">id of a customer</param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            BO.Customer boCustomer = new BO.Customer();
            try
            {
                DO.Customer doCustomerd = idal.GetCustomer(id);
                boCustomer.Id = doCustomerd.Id;
                boCustomer.Name = doCustomerd.Name;
                boCustomer.Phone = doCustomerd.Phone; 
                BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boCustomer.Location = new Location
                {
                    Latitude = doCustomerd.Latitude,
                    Longitude = doCustomerd.Longitude
                };
                boCustomer.ParcelsFromCustomer = GetParcelsFromCustomer(id);
                boCustomer.ParcelsToCustomer = GetParcelsToCustomer(id);

            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boCustomer;
        }

        /// <summary>
        /// a function that gets an id of a customer returns the parcels he sent
        /// </summary>
        /// <param name="id">id of a customer</param>
        /// <returns></returns>
        public IEnumerable<ParcelAtCustomer> GetParcelsFromCustomer(int id)
        {
            try
            {
                DO.Customer c = idal.GetCustomer(id);
                return
                from item in idal.AllParcel()
                where item.SenderId == id
                let cstReceiver = idal.GetCustomer(item.TargetId)
                let droneInParcel = ListBLDrones.Find(d => d.Id == id)
                select new ParcelAtCustomer
                {
                    Id = item.Id,//id of parcel
                    Weight = (BO.WeightCategories)item.Weight,
                    Priority = (BO.Priorities)item.Priority,
                    ParcelState = getParcelState(item.Id),
                    customer = new CustomerOfParcel
                    {
                        Id = item.TargetId,
                        Name = idal.GetCustomer(item.TargetId).Name
                    }
                };
            }

            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that gets an id of a customer returns the parcels he recieved
        /// </summary>
        /// <param name="id">id of a customer</param>
        /// <returns></returns>
        public IEnumerable<ParcelAtCustomer> GetParcelsToCustomer(int id)
        {
            try
            {
                DO.Customer c = idal.GetCustomer(id);
                return
                from item in idal.AllParcel()
                where item.TargetId == id
                let cstSender = idal.GetCustomer(item.TargetId)
                let droneInParcel = ListBLDrones.Find(d => d.Id == id)
                select new ParcelAtCustomer
                {
                    Id = item.Id,//id of parcel
                    Weight = (BO.WeightCategories)item.Weight,
                    Priority = (BO.Priorities)item.Priority,
                    ParcelState = getParcelState(item.Id),
                    customer = new CustomerOfParcel
                    {
                        Id = item.SenderId,
                        Name = idal.GetCustomer(item.SenderId).Name
                    },

                };
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }

        /// <summary>
        /// a function that return BL dronescharging of a station
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns></returns>
        public IEnumerable<DroneCharging> GetDroneChargingPerStation(int id)
        {
            return (from item in idal.GetDroneChargeInStation(id)
                    select new DroneCharging
                    {
                        Id = id,
                        BattaryStatus = ListBLDrones.Find(d => id == item.DroneId).BatteryStatus
                    }).ToList(); ;

        }

        /// <summary>
        /// a function that returns the custoner of a parcel
        /// </summary>
        /// <param name="id">id of a customer</param>
        /// <returns></returns>
        private CustomerOfParcel getCustomerOfParcel(int id)
        {
            try
            {
                CustomerOfParcel cp = new CustomerOfParcel();
                DO.Customer doCast = new DO.Customer();
                doCast = idal.GetCustomer(id);
                cp.Name = doCast.Name;
                cp.Id = id;
                return cp;
            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that gets an id of a pacel and returns the parcel
        /// </summary>
        /// <param name="id">id of a parcel</param>
        /// <returns></returns>
        public Parcel GetParcel(int id)
        {
            BO.Parcel boParcel = new BO.Parcel();
            try
            {
                DO.Parcel doParcel = idal.GetParcel(id);
                boParcel.Id = doParcel.Id;
                boParcel.Sender = new CustomerOfParcel
                {
                    Id = doParcel.SenderId,//by parcel id
                    Name = idal.GetCustomer(doParcel.SenderId).Name,//by customer id who is the sender in the parcel
                };
                boParcel.Receiver = new CustomerOfParcel
                {
                    Id = doParcel.TargetId,//by parcel id
                    Name = idal.GetCustomer(doParcel.TargetId).Name,//by customer id who is the sender in the parcel
                };
                boParcel.Weight = (WeightCategories)doParcel.Weight;
                boParcel.Priority = (Priorities)doParcel.Priority;
                boParcel.ParcelDrones = new DroneInParcel
                {
                    Id= doParcel.DroneId,
                    BattaryStatus = ListBLDrones.Find(d => d.Id == doParcel.DroneId).BatteryStatus,
                    Location = ListBLDrones.Find(d => d.Id == doParcel.DroneId).Location,
                };
                    
                boParcel.RequestedTime = doParcel.RequestedTime;
                boParcel.ScheduledTime = doParcel.ScheduledTime;
                boParcel.PickedUpTime = doParcel.PickedUpTime;
                boParcel.DeliveredTime = doParcel.DeliveredTime;

            }
            catch (DO.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boParcel;
        }
        //public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        //{
        //    idal.GetDroneInParcelByPredicate(predicate);
        //}

    }

}
