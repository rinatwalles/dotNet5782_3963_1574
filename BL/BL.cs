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

        DAL.IDAL.IDAL idal;
        public BL()
        {
            idal = new DalObject.DalObject();
            // double ChargePrecent = DalObject.DataSource.Config.ChargePrecent; בגלל המערך שאנחנו צריכות לעשות בDALOBJECT
            //השדה של צריכת חשמל 
            IEnumerable<IDAL.DO.Drone> drones = idal.AllDrones();
        }
        List<IBL.BO.DroneToList> ListBLDrones { get; set; }  //למה בלי pyblic????????????

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
                doDrone.MaxWeight = (IDAL.DO.WeightCategories)d.Weight;
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
                if (idal.CheckStation(s.Id))
                    throw new DuplicateIdException(s.Id, "Drone");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
                IDAL.DO.Station doStation = new IDAL.DO.Station();
                doStation.Id = s.Id;
                doStation.Name = s.Name;
                doStation.Latitude = s.SLocation.Latitude;
                doStation.Longitude = s.SLocation.Longitude;
                doStation.ChargeSlots = s.AvilableChargeSlotsNumber;
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
                    throw new DuplicateIdException(c.Id, "Drone");// מסכים רק DAL

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
                    throw new DuplicateIdException(p.Id, "Drone");// מסכים רק DAL

                //הצבת ערכים בשביל לשמור את האוביקט בDAL
                IDAL.DO.Parcel doParcel = new IDAL.DO.Parcel()
                {
                    SenderId = IdSender,
                    TargetId = IdReceiver,
                    Weight = (IDAL.DO.WeightCategories)p.Weight,
                    Priority = (IDAL.DO.Priorities)p.Priority,
                    DroneId = 0,
                    Requested = DateTime.MinValue,
                    Scheduled = DateTime.MinValue,
                    PickedUp = DateTime.MinValue,
                    Delivered = DateTime.MinValue
                };
                idal.ParcelAddition(doParcel);
            }
            //catch DO.exc
            catch (DAL.DuplicateIdException ex)
            {
                throw new DuplicateIdException(ex.ID, ex.EntityName);
            }
        }
        public BaseStation getBaseStation(int id)
        {
            IBL.BO.BaseStation boBaseStation = new IBL.BO.BaseStation();
            try
            {
                IDAL.DO.Station doStation = idal.GetStation(id);
                boBaseStation.Id = doStation.Id;
                boBaseStation.Name = doStation.Name;
                boBaseStation.SLocation = new Location
                {
                    Latitude = doStation.Latitude,
                    Longitude = doStation.Longitude
                };
                boBaseStation.AvilableChargeSlotsNumber = doStation.ChargeSlots;
                boBaseStation
            }
        }
        public Drone GetDrone(int id)
        {
            IBL.BO.Drone boDrone = new IBL.BO.Drone();
            try
            {
                IDAL.DO.Drone doDrone = idal.GetDrone(id);
                boDrone.Id = doDrone.Id;
                boDrone.Model = doDrone.Model;
                boDrone.Weight = (IBL.BO.WeightCategories)doDrone.MaxWeight;//באיידאל זה מקסימום וויט ואיי בי אל זה סתם וויט בלי מקסימום 
                IBL.BO.DroneToList dtl= ListBLDrones.Find(d => d.Id == id);
                boDrone.BatteryStatus = dtl.BatteryStatus;
                boDrone.DroneStatus = dtl.DroneStatus;
                boDrone.Location = dtl.Location;
                //boDrone.ParcelInDelivery מה עם זה
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boDrone;
        }

    }
}
