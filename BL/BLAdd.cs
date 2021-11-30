using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IBL.BO;
using BL;
using static DalObject.DataSource;

namespace BL
{
    public partial class BL : IBL.IBL
    { 
        static Random rand = new Random(DateTime.Now.Millisecond);

        internal static DAL.IDAL.IDAL idal;
        public List<IBL.BO.DroneToList> ListBLDrones;
        public BL()
        {
            idal = new DalObject.DalObject();

            double[] array = idal.AskingElectricityUse();
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
                                                   Weight = (WeightCategories)dodron.Weight
                                               });
            DateTime t = DateTime.MinValue;
            Location locat = new Location();
            foreach (DroneToList item in ListBLDrones)
            {
                if (idal.AllParcel().Any(parc => ((parc.DroneId == item.Id) && (parc.DeliveredTime == t))))
                {
                    item.DroneStatus = DroneStatuses.Delivery;
                    IDAL.DO.Parcel parc = idal.GetParcel(item.ParcelNumber);
                    IDAL.DO.Customer cust = idal.GetCustomer(parc.SenderId);
                    locat.Latitude = cust.Latitude;
                    locat.Longitude = cust.Longitude;
                    if (parc.PickedUpTime == t)    //parcel schduled but not PickedUp
                        item.Location = MinDistanceOfSation(locat);    //the location is the closest station
                    if (parc.DeliveredTime == t)   //the parcel not deliverd so the location is the sender location
                        item.Location = locat;

                    double calculate = idal.DistanceCalculate(cust.Longitude, cust.Latitude, item.Location.Longitude, item.Location.Latitude);
                    Location closeStation = MinDistanceOfSation(locat);

                    calculate += idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];
                    item.BatteryStatus = rand.NextDouble() + rand.Next((int)calculate, 100);

                }
                else if (!idal.AllParcel().Any(parc => (parc.DroneId == item.Id)))    //not doing a delivery now
                    item.DroneStatus = (DroneStatuses)(rand.Next(0, 1) * 2);    //0 or 2
                else if (item.DroneStatus == DroneStatuses.Maintenance)   //drone in maintance
                {
                    int id = rand.Next(1, 2);
                    IDAL.DO.Station stat = idal.GetStation(id);
                    locat.Longitude = stat.Longitude;
                    locat.Latitude = stat.Latitude;
                    item.Location = locat;
                    item.BatteryStatus = rand.NextDouble() * 20;
                }
                else if (item.DroneStatus == DroneStatuses.Available)   //the drone is available
                {
                    foreach (IDAL.DO.Parcel parc in idal.AllParcel())
                    {
                        if (parc.DeliveredTime != t)
                        {
                            IDAL.DO.Customer cust = idal.GetCustomer(parc.SenderId);
                            locat.Latitude = cust.Latitude;
                            locat.Longitude = cust.Longitude;
                            Location closeStation = MinDistanceOfSation(locat);
                            double calculate = idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];
                            item.BatteryStatus = rand.NextDouble() + rand.Next((int)calculate, 100);
                        }
                    }
                }
            }
        }
        /function that gets a location and returns the closest station
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
        //function that gets a location and returns the closest station
        //private Station MinDistanceOfSation(Location locat)
        //{
        //    double[] array = idal.AskingElectricityUse();
        //    IDAL.DO.Station minStation = new IDAL.DO.Station();
        //    //Location newlocat=new Location();
        //    double minDistance = 1000;
        //    foreach (IDAL.DO.Station item in idal.GetStationByPredicate(st => st.AvailableChargeSlots > 0))  //searching the closest station to the sender
        //    {
        //        double dist = idal.DistanceCalculate(item.Longitude, item.Latitude, locat.Longitude, locat.Latitude);
        //        if(idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight])
        //        if (dist < minDistance)
        //        {
        //            minStation = item;
        //            //newlocat.Latitude = item.Latitude;
        //            //newlocat.Longitude = item.Longitude;
        //            //minDistance = dist;
        //        }
        //    }
        //    return getBaseStation(minStation.Id);
        //}

            public void AddDrone(Drone d, int sId)
            {
            try
            {
                d.BatteryStatus = rand.NextDouble() * 100;//לא יתן 1, רק קטן מ1!
                d.DroneStatus = DroneStatuses.Maintenance;
                d.Location.Latitude = idal.GetStation(sId).Latitude;
                d.Location.Longitude = idal.GetStation(sId).Longitude;
                //בדיקה האם הרחפן קיים כבר
                if (idal.CheckDrone(d.Id))
                    throw new DuplicateIdException(d.Id, "Drone");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
                IDAL.DO.Drone doDrone = new IDAL.DO.Drone();
                doDrone.Id = d.Id;
                doDrone.Model = d.Model;
                doDrone.Weight = (IDAL.DO.WeightCategories)d.Weight;
                idal.DroneAddition(doDrone);

                //addtp bl list
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
        public void AddBaseStation(Station s)
        {
            try
            {
                //בדיקה האם הרחפן קיים כבר
                if (idal.CheckStation(s.Id))               //למה צריך לבדוק גם פה? זה נבדק גם בפונקצית הוספה בDAL?????
                    throw new DuplicateIdException(s.Id, "Station");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
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
        public void AddCustomer(Customer c)
        {
            try
            {
                //בדיקה האם הרחפן קיים כבר
                if (idal.CheckCustomer(c.Id))
                    throw new DuplicateIdException(c.Id, "Customer");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
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
        public void AddParcel(Parcel p, int IdSender, int IdReceiver)
        {
            try
            {
                //בדיקה האם הרחפן קיים כבר
                if (idal.CheckParcel(p.Id))
                    throw new DuplicateIdException(p.Id, "Parcel");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
                IDAL.DO.Parcel doParcel = new IDAL.DO.Parcel()
                {
                    SenderId = IdSender,
                    TargetId = IdReceiver,
                    Weight = (IDAL.DO.WeightCategories)p.Weight,
                    Priority = (IDAL.DO.Priorities)p.Priority,
                    DroneId = 0,
                    RequestedTime = DateTime.MinValue,
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




