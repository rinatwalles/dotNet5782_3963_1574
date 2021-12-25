using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IBL.BO;
using BL;

namespace BL
{
    public partial class BL : IBL.IBL
    { 
        static Random rand = new Random(DateTime.Now.Millisecond);

        internal static DAL.IDAL.IDAL idal;
        public List<IBL.BO.DroneToList> ListBLDrones = new List<IBL.BO.DroneToList>();
        double[] array;

        /// <summary>
        /// Constructor of BL
        /// </summary>
        public BL()
        {
            idal = new DalObject.DalObject();

            array = idal.AskingElectricityUse();
            double Available = array[0];
            double Light = array[1];
            double Medium = array[2];
            double Heavy = array[3];
            double ChargePrecent = array[4];

            ListBLDrones = (List<DroneToList>)(from dodron in idal.AllDrones()
                                               select new DroneToList()
                                               {
                                                   Id = dodron.Id,
                                                   Model = dodron.Model,
                                                   Weight = (WeightCategories)dodron.Weight,
                                                   ParcelNumber= rand.Next(1, 5)
                                               }).ToList(); ;
            Location locat = new Location();
            try
            {
                foreach (DroneToList item in ListBLDrones)
                {
                    if (idal.AllParcel().Any(parc => ((parc.DroneId == item.Id) && (parc.DeliveredTime == DateTime.MinValue))))
                    {
                        item.DroneStatus = DroneStatuses.Delivery;
                        IDAL.DO.Parcel parc = idal.GetParcel(item.ParcelNumber);
                        IDAL.DO.Customer cust = idal.GetCustomer(parc.SenderId);
                        locat.Latitude = cust.Latitude;
                        locat.Longitude = cust.Longitude;
                        item.Location = locat;
                        if (parc.PickedUpTime == null)    //parcel schduled but not PickedUp
                            item.Location = MinDistanceOfSation(locat);    //the location is the closest station
                        if (parc.DeliveredTime == null)   //the parcel not deliverd so the location is the sender location
                            item.Location = locat;

                        double calculate = idal.DistanceCalculate(cust.Longitude, cust.Latitude, item.Location.Longitude, item.Location.Latitude) * array[1 + (int)item.Weight]; ;
                        Location closeStation = MinDistanceOfSation(locat);

                        calculate += idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];
                        item.BatteryStatus = rand.NextDouble() + rand.Next((int)calculate, 100);

                    }
                    else if (!idal.AllParcel().Any(parc => (parc.DroneId == item.Id)))    //not doing a delivery now
                    {
                        item.DroneStatus = (DroneStatuses)(rand.Next(0, 2) * 2);    //0 or 2
                    }
                    if (item.DroneStatus == DroneStatuses.Maintenance)   //drone in maintance
                    {
                        int id = rand.Next(1, 2);
                        IDAL.DO.Station stat = idal.GetStation(id);
                        locat.Longitude = stat.Longitude;
                        locat.Latitude = stat.Latitude;
                        item.Location = locat;
                        item.BatteryStatus = rand.NextDouble() * 20;
                        //item.DroneStatus = DroneStatuses.Available;
                        //droneToCharge(item.Id);
                    }
                    
                    if (item.DroneStatus == DroneStatuses.Available)   //the drone is available
                    {
                    int randId = rand.Next(11, 16);
                    IDAL.DO.Parcel doParcel =idal.GetParcel(randId);    
                    IDAL.DO.Customer cust = idal.GetCustomer(doParcel.SenderId);
                    locat.Latitude = cust.Latitude;
                    locat.Longitude = cust.Longitude;
                    Location closeStation = MinDistanceOfSation(locat);
                    item.Location = closeStation;
                    double calculate = idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];
                    item.BatteryStatus = rand.NextDouble() + rand.Next((int)calculate, 100);
                            
                    }
                }
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        ///  function that gets a location and returns the closest station
        /// </summary>
        /// <param name="locat">a location we want to find the closest station from</param>
        /// <returns></returns>
        private Location MinDistanceOfSation(Location locat)
        {
            Location newlocat = new Location();
            double minDistance = 1000;
            foreach (IDAL.DO.Station stat in idal.AllStation())  //searching the closest station to the sender
            {
                double dist = idal.DistanceCalculate(stat.Longitude, stat.Latitude, locat.Longitude, locat.Latitude);
                if (dist < minDistance)
                {
                    newlocat.Latitude = stat.Latitude;
                    newlocat.Longitude = stat.Longitude;
                    minDistance = dist;
                }
            }
            return newlocat;
        }

        /// <summary>
        /// function add drone  to data and to the list in BL
        /// </summary>
        /// <param name="d"> a drone of BL layer</param>
        /// <param name="sId"> id of a station we want to place the drone in</param>
        public void AddDrone(Drone d, int sId)
        {
            try
            {
                d.BatteryStatus = rand.NextDouble() * 100;//לא יתן 1, רק קטן מ1!
                d.DroneStatus = DroneStatuses.Maintenance;
                Location locat = new Location();

               locat.Latitude = idal.GetStation(sId).Latitude;
               locat.Longitude = idal.GetStation(sId).Longitude;
                d.Location = locat;
                //check if the drone is already exists
                if (idal.CheckDrone(d.Id))
                    throw new DuplicateIdException(d.Id, "Drone");

                //adding the drone to DAL layer and addupt the feilds
                IDAL.DO.Drone doDrone = new IDAL.DO.Drone();
                doDrone.Id = d.Id;
                doDrone.Model = d.Model;
                doDrone.Weight = (IDAL.DO.WeightCategories)d.Weight;
                idal.DroneAddition(doDrone);

                //addutp bl list
                DroneToList Dl = new IBL.BO.DroneToList()
                {
                    Id = d.Id,
                    Model = d.Model,
                    BatteryStatus = d.BatteryStatus,
                    DroneStatus = d.DroneStatus,
                    Location = d.Location,
                    Weight = d.Weight
                };
                ListBLDrones.Add(Dl);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that adds a station to the DAL layer
        /// </summary>
        /// <param name="s">the station we eant to add</param>
        public void AddStation(Station s)
        {
            try
            {
                //check if the station is already exists
                if (idal.CheckStation(s.Id))
                    throw new DuplicateIdException(s.Id, "Station");

                //adding the station to DAL layer and addupt the feilds
                IDAL.DO.Station doStation = new IDAL.DO.Station();
                doStation.Id = s.Id;
                doStation.Name = s.Name;
                doStation.Latitude = s.Location.Latitude;
                doStation.Longitude = s.Location.Longitude;
                doStation.AvailableChargeSlots = s.AvailableChargeSlots;
                idal.StationAddition(doStation);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that adds a customer to the DAL layer
        /// </summary>
        /// <param name="c">the customer we want to add</param>
        public void AddCustomer(Customer c)
        {
            try
            {
                //check if the customer is already exists
                if (idal.CheckCustomer(c.Id))
                    throw new DuplicateIdException(c.Id, "Customer");

                //adding the customer to DAL layer and addupt the feilds
                IDAL.DO.Customer doCustomer = new IDAL.DO.Customer();
                doCustomer.Id = c.Id;
                doCustomer.Name = c.Name;
                doCustomer.Latitude = c.Location.Latitude;
                doCustomer.Longitude = c.Location.Longitude;
                doCustomer.Phone = c.Phone;
                idal.CustomerAddition(doCustomer);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a function that adds a parcel to the DAL layer
        /// </summary>
        /// <param name="p">the parcel we want to add to the dal</param>
        /// <param name="IdSender">the id of the customer who sent the parcel</param>
        /// <param name="IdReceiver">the id of the customer who receive the parcel</param>
        public void AddParcel(Parcel p, int IdSender, int IdReceiver)
        {
            try
            {
                //check if the parcel is already exists
                if (idal.CheckParcel(p.Id))
                    throw new DuplicateIdException(p.Id, "Parcel");

                //adding the parcel to DAL layer and addupt the feilds
                IDAL.DO.Parcel doParcel = new IDAL.DO.Parcel()
                {
                    SenderId = IdSender,
                    TargetId = IdReceiver,
                    Weight = (IDAL.DO.WeightCategories)p.Weight,
                    Priority = (IDAL.DO.Priorities)p.Priority,
                    DroneId = 0,
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.MinValue,
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                idal.ParcelAddition(doParcel);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
    }
}




