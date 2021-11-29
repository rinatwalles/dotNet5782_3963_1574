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
                IBL.BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boDrone.BatteryStatus = dtl.BatteryStatus;
                boDrone.DroneStatus = dtl.DroneStatus;
                boDrone.Location = dtl.Location;
                //boDrone.ParcelInDelivery מה עם זה
                if(boDrone.DroneStatus== IBL.BO.DroneStatuses.Delivery)
                {
                    boDrone.ParcelInDelivery = new ParcelInDelivery
                    {
                        Id=idal.getParcelByDroneId(id).Id,
                        SenderName= new CustomerOfParcel
                        {
                            Id= idal.GetParcel(boDrone.ParcelInDelivery.Id).SenderId,//by parcel id
                            Name = idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Name,//by customer id who is the sender in the parcel
                        },
                        ReceiverName = new CustomerOfParcel
                        {
                            Id = idal.GetParcel(boDrone.ParcelInDelivery.Id).TargetId,//by parcel id
                            Name = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Name,//by customer id who is the reciever in the parcel
                        },
                        Weight=(WeightCategories)idal.GetParcel(boDrone.ParcelInDelivery.Id).Weight,
                        Priority= (Priorities)idal.GetParcel(boDrone.ParcelInDelivery.Id).Priority,
                        CollectingPlace= new Location
                        {
                            Longitude= idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Longitude,//locationr of the sender by parcel, sender, customer name
                            Latitude= idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Latitude
                        },
                        DestinationPlace = new Location
                        {
                            Longitude = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Longitude,//locationr of the reciever by parcel, sender, customer name
                            Latitude = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Latitude
                        },
                        TransportDistance=
                    };
                    boDrone.ParcelInDelivery.ParcelState = getParcelState(id) == ParcelStates.PickedUp;//החבילה נאספה והיא בדרך
                       
                }
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
            return boDrone;
        }
        private ParcelStates getParcelState(int id)
        {
            IDAL.DO.Parcel doParcel = idal.GetParcel(id);
            if (DateTime.Now < doParcel.ScheduledTime)
                return ParcelStates.Requested;
            else
            {
                if (DateTime.Now >= doParcel.ScheduledTime && DateTime.Now < doParcel.PickedUpTime)
                    return ParcelStates.Scheduled;
                else
                    if (DateTime.Now >= doParcel.PickedUpTime && DateTime.Now < doParcel.DeliveredTime)
                    return ParcelStates.PickedUp;
            }
            return ParcelStates.Delivered;
        }

        public Customer GetCustomer(int id)
        {
            IBL.BO.Customer boCustomer = new IBL.BO.Customer();
            try
            {
                IDAL.DO.Customer doCustomerd = idal.GetCustomer(id);
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





        private IEnumerable<ParcelAtCustomer> GetParcelsFromCustomer(int id)
        {
            IDAL.DO.Customer c = idal.GetCustomer(id);
            return
            from item in idal.AllParcel()
            where item.SenderId == id
            let cstReceiver = idal.GetCustomer(item.TargetId)
            let droneInParcel = ListBLDrones.Find(d => d.Id == id)
            select new ParcelAtCustomer
            {
                Id = item.Id,
                Weight = (IBL.BO.WeightCategories)item.Weight,
                Priority = (IBL.BO.Priorities)item.Priority,
                
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
        private IEnumerable<DroneCharging> GetDroneChargingPerStation(int id)
        {//עשיתי פונקציה שעושה מה שרצית לעשות פה. אם הבנתי מה רצית
            return
            from item in idal.CountDroneCharge(id)
            where item.DroneId == id
            select new DroneCharging
            {
                Id = id,
                BattaryStatus = ListBLDrones.Find(d => id == item.DroneId).BatteryStatus
            };

        }

        

        //פןנקציה פרטית שתחזיר לי אוביקט ממש מהסןג הזה 
        //בנוסף היא יכולה להיות PRIVATE לבדוק את הנושא כי זכור לי משהו שדן כתב
        //יש דרך אחרת??
        public CustomerOfParcel getCustomerOfParcel(int id)
        {
            CustomerOfParcel cp = new CustomerOfParcel();
            IDAL.DO.Customer doCast = new IDAL.DO.Customer();
            doCast = idal.GetCustomer(id);
            cp.Name = doCast.Name;
            cp.Id = id;
            return cp;
        }
    }
}
