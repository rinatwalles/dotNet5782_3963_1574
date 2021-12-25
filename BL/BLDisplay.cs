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
        /// <summary>
        ///  a function that gets an id of a station returns the station
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns></returns>
        public Station GetStation(int id)
        {
            IBL.BO.Station boBaseStation = new IBL.BO.Station();
            try
            {
                IDAL.DO.Station doStation = idal.GetStation(id);
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
            catch (DAL.MissingIdException ex)
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
            IBL.BO.Drone boDrone = new IBL.BO.Drone();
            try
            {
                IDAL.DO.Drone doDrone = idal.GetDrone(id);
                boDrone.Id = doDrone.Id;
                boDrone.Model = doDrone.Model;
                boDrone.Weight = (IBL.BO.WeightCategories)doDrone.Weight;
                IBL.BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boDrone.BatteryStatus = dtl.BatteryStatus;
                boDrone.DroneStatus = dtl.DroneStatus;
                boDrone.Location = dtl.Location;
                //boDrone.ParcelInDelivery מה עם זה
                if (boDrone.DroneStatus == IBL.BO.DroneStatuses.Delivery)
                {
                    IDAL.DO.Parcel boParcel = idal.getParcelByDroneId(id);
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

                }
            }
            catch (DAL.MissingIdException ex)
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
                IDAL.DO.Parcel doParcel = idal.GetParcel(id);
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
            catch (DAL.MissingIdException ex)
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
            IBL.BO.Customer boCustomer = new IBL.BO.Customer();
            try
            {
                IDAL.DO.Customer doCustomerd = idal.GetCustomer(id);
                boCustomer.Id = doCustomerd.Id;
                boCustomer.Name = doCustomerd.Name;
                boCustomer.Phone = doCustomerd.Phone; 
                IBL.BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boCustomer.Location = new Location
                {
                    Latitude = doCustomerd.Latitude,
                    Longitude = doCustomerd.Longitude
                };
                boCustomer.ParcelsFromCustomer = GetParcelsFromCustomer(id);
                boCustomer.ParcelsToCustomer = GetParcelsToCustomer(id);

            }
            catch (DAL.MissingIdException ex)
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
        private IEnumerable<ParcelAtCustomer> GetParcelsFromCustomer(int id)
        {
            try
            {
                IDAL.DO.Customer c = idal.GetCustomer(id);
                return
                from item in idal.AllParcel()
                where item.SenderId == id
                let cstReceiver = idal.GetCustomer(item.TargetId)
                let droneInParcel = ListBLDrones.Find(d => d.Id == id)
                select new ParcelAtCustomer
                {
                    Id = item.Id,//id of parcel
                    Weight = (IBL.BO.WeightCategories)item.Weight,
                    Priority = (IBL.BO.Priorities)item.Priority,
                    ParcelState = getParcelState(item.Id),
                    customer = new CustomerOfParcel
                    {
                        Id = item.TargetId,
                        Name = idal.GetCustomer(item.TargetId).Name
                    }
                };
            }

            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that gets an id of a customer returns the parcels he recieved
        /// </summary>
        /// <param name="id">id of a customer</param>
        /// <returns></returns>
        private IEnumerable<ParcelAtCustomer> GetParcelsToCustomer(int id)
        {
            try
            {
                IDAL.DO.Customer c = idal.GetCustomer(id);
                return
                from item in idal.AllParcel()
                where item.TargetId == id
                let cstSender = idal.GetCustomer(item.TargetId)
                let droneInParcel = ListBLDrones.Find(d => d.Id == id)
                select new ParcelAtCustomer
                {
                    Id = item.Id,//id of parcel
                Weight = (IBL.BO.WeightCategories)item.Weight,
                    Priority = (IBL.BO.Priorities)item.Priority,
                    ParcelState = getParcelState(item.Id),
                customer = new CustomerOfParcel
                    {
                        Id = item.SenderId,
                        Name = idal.GetCustomer(item.SenderId).Name
                    },

                };
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }

        /// <summary>
        /// a function that return BL dronescharging of a station
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns></returns>
        private IEnumerable<DroneCharging> GetDroneChargingPerStation(int id)
        {
            return
            from item in idal.CountDroneCharge(id)
            select new DroneCharging
            {
                Id = id,
                BattaryStatus = ListBLDrones.Find(d => id == item.DroneId).BatteryStatus
            };

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
                IDAL.DO.Customer doCast = new IDAL.DO.Customer();
                doCast = idal.GetCustomer(id);
                cp.Name = doCast.Name;
                cp.Id = id;
                return cp;
            }
            catch (DAL.MissingIdException ex)
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
            IBL.BO.Parcel boParcel = new IBL.BO.Parcel();
            try
            {
                IDAL.DO.Parcel doParcel = idal.GetParcel(id);
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
            catch (DAL.MissingIdException ex)
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
