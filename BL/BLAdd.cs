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

        internal DAL.IDAL.IDAL idal;
        List<IBL.BO.DroneToList> ListBLDrones;
        public BL()
        {
            idal = new DalObject.DalObject();

            double[] array = idal.AskingElectricityUse();
            double Available = array[0];
            double Light = array[1];
            double Medium = array[2];
            double Heavy = array[3];
            double ChargePrecent = array[4];

           // IEnumerable<IDAL.DO.Drone> drones = idal.AllDrones();
        //    public List<IBL.BO.DroneToList> ListBLDrones { get; set; }

         ListBLDrones=
                   from doparc in idal.AllDrones()
                   select new DroneToList()
                   {

                   };
        }

      

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
                    Weight = d.Weight,
                    PassedParcelNumber = 0
                };
                ListBLDrones.Add(Dl);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public void AddBaseStation(BaseStation s)
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
                doStation.Latitude = s.SLocation.Latitude;
                doStation.Longitude = s.SLocation.Longitude;
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
                doCustomer.Latitude = c.Place.Latitude;
                doCustomer.Longitude = c.Place.Longitude;
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
