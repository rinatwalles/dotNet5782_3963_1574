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

            double[] array = idal.AskingElectricityUse();
            double Available = array[0];
            double Light = array[1];
            double Medium = array[2];
            double Heavy = array[3];
            double ChargePrecent = array[4];

            IEnumerable<IDAL.DO.Drone> drones = idal.AllDrones();
        }
        public List<IBL.BO.DroneToList> ListBLDrones { get; set; }  

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
                if (idal.CheckStation(s.Id))               //למה צריך לבדוק גם פה? זה נבדק גם בפונקצית הוספה בDAL?????
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
                boBaseStation.DroneCharging = GetDroneChargingPerStation(id);
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boBaseStation;
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
        Customer GetCustomer(int id)
        {
            IBL.BO.Customer boCustomer = new IBL.BO.Customer();
            try
            {
                IDAL.DO.Customer doCustomerd= idal.GetCustomer(id);
                boCustomer.Id = doCustomerd.Id;
                boCustomer.Name = doCustomerd.Name;
                boCustomer.Phone = doCustomerd.Phone;//באיידאל זה מקסימום וויט ואיי בי אל זה סתם וויט בלי מקסימום 
                IBL.BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boCustomer.Place = new Location
                {
                    Latitude = doCustomerd.Latitude,
                    Longitude = doCustomerd.Longitude
                };
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

        public void UpdateCustomer(int id, string name, string phone )
        {
            try
            {
                IDAL.DO.Customer doCustomer  = new IDAL.DO.Customer();
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








        //תצגוגת של רשימות
        public IEnumerable<BaseStationToList> GetAllBaseStations()
        {
            return from dostat in idal.AllStation()
                   select new BaseStationToList()
                   {
                       Id = dostat.Id,
                       Name = dostat.Name,
                       AvailableCharginggSlotsNumber = dostat.ChargeSlots,
                       RservedCharginggSlotsNumber = idal.CountDroneCharge(dostat.Id).Count()
                   };
        }

        public IEnumerable<DroneToList> GetAllDrones()
        {
            return from dodrone in ListBLDrones
                   select new DroneToList();
        }

        private IEnumerable<Parcel> GetParcelsFromCustomer(int id)
        {
            IDAL.DO.Customer c = idal.GetCustomer(id);
            return
            from item in DalObject.DataSource.parcels
            where item.SenderId == id
            let cstReceiver = idal.GetCustomer(item.TargetId)
            let droneInParcel= ListBLDrones.Find(d => d.Id == id)
            select new Parcel
            {
                Id = item.Id,
                Sender = new CustomerOfParcel
                {
                    Id = id,
                    Name = c.Name
                },
               
                Receiver = new CustomerOfParcel
                {
                    Id = cstReceiver.Id,
                    Name = cstReceiver.Name
                },
                Weight=(IBL.BO.WeightCategories)item.Weight,
                Priority= (IBL.BO.Priorities)item.Priority,
                ParcelsDrones = new DroneInParcel
                {
                    Id = droneInParcel.Id,
                    BattaryStatus= droneInParcel.BatteryStatus

                },
            };
        }

   
    //Parcel getParcel ()
    //{
    //    {
    //        Id = item.Id,
    //            Sender = new CustomerOfParcel
    //            {
    //                Id = id,
    //                Name = c.Name
    //            },
               
    //            Receiver = new CustomerOfParcel
    //            {
    //                Id = cstReceiver.Id,
    //                Name = cstReceiver.Name
    //            },
    //            Weight = (IBL.BO.WeightCategories)item.Weight,
    //            Priority = (IBL.BO.Priorities)item.Priority,
    //}
        private IEnumerable<Parcel> GetDroneChargingPerStation(int id)
        {
            return
            from item in DalObject.DataSource.droneCharges
            where item.DroneId == id
            select new DroneCharging
            {
                Id = id,
                BattaryStatus = ListBLDrones.Find(d => id == item.DroneId).BatteryStatus
            };

        }

        public IEnumerable<CustomerToList> GetAllCustomers()
        {
            return from docust in idal.AllCustomer()
                   select new CustomerToList()
                   {
                       Id = docust.Id,
                       Name = docust.Name,
                       Phone = docust.Phone,
                       //NumOfParcelsSentNotSupplied
                       //NumOfParcelsSentAndSupplied
                       //NumOfParcelsDelivered
                       //NumOfParcelsReceived
                   };
        }

        //פןנקציה פרטית שתחזיר לי אוביקט ממש מהסןג הזה 
        //בנוסף היא יכולה להיות PRIVATE לבדוק את הנושא כי זכור לי משהו שדן כתב
        //יש דרך אחרת??
       public CustomerOfParcel getCustomerOfParcel(int id)
        {
            CustomerOfParcel cp = new CustomerOfParcel();
            IDAL.DO.Customer doCast = new IDAL.DO.Customer();
            doCast=idal.GetCustomer(id);
            cp.Name = doCast.Name;
            cp.Id = id;
            return cp;
        }

        public IEnumerable<ParcelToList> GetAllParcels()
        {
            return from doparc in idal.AllParcel()
                   select new ParcelToList()
                   {
                       Id = doparc.Id,
                       Sender = getCustomerOfParcel(doparc.SenderId),
                       Receiver = getCustomerOfParcel(doparc.TargetId),
                       Weight = (WeightCategories)doparc.Weight,
                       Priority = (Priorities)doparc.Priority,
                       //ParcelState=doparc.
                    };
        }

    }
}
