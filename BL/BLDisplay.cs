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
        
        public Station getBaseStation(int id)
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

        public Drone GetDrone(int id)
        {
            IBL.BO.Drone boDrone = new IBL.BO.Drone();
            try
            {
                IDAL.DO.Drone doDrone = idal.GetDrone(id);
                boDrone.Id = doDrone.Id;
                boDrone.Model = doDrone.Model;
                boDrone.Weight = (IBL.BO.WeightCategories)doDrone.Weight;//באיידאל זה מקסימום וויט ואיי בי אל זה סתם וויט בלי מקסימום 
                IBL.BO.DroneToList dtl = ListBLDrones.Find(d => d.Id == id);
                boDrone.BatteryStatus = dtl.BatteryStatus;
                boDrone.DroneStatus = dtl.DroneStatus;
                boDrone.Location = dtl.Location;
                //boDrone.ParcelInDelivery מה עם זה
                if (boDrone.DroneStatus == IBL.BO.DroneStatuses.Delivery)
                {
                    boDrone.ParcelInDelivery = new ParcelInDelivery
                    {
                        Id = idal.getParcelByDroneId(id).Id,
                        SenderName = new CustomerOfParcel
                        {
                            Id = idal.GetParcel(boDrone.ParcelInDelivery.Id).SenderId,//by parcel id
                            Name = idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Name,//by customer id who is the sender in the parcel
                        },
                        ReceiverName = new CustomerOfParcel
                        {
                            Id = idal.GetParcel(boDrone.ParcelInDelivery.Id).TargetId,//by parcel id
                            Name = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Name,//by customer id who is the reciever in the parcel
                        },
                        Weight = (WeightCategories)idal.GetParcel(boDrone.ParcelInDelivery.Id).Weight,
                        Priority = (Priorities)idal.GetParcel(boDrone.ParcelInDelivery.Id).Priority,
                        CollectingPlace = new Location
                        {
                            Longitude = idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Longitude,//locationr of the sender by parcel, sender, customer name
                            Latitude = idal.GetCustomer(boDrone.ParcelInDelivery.SenderName.Id).Latitude
                        },
                        DestinationPlace = new Location
                        {
                            Longitude = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Longitude,//locationr of the reciever by parcel, sender, customer name
                            Latitude = idal.GetCustomer(boDrone.ParcelInDelivery.ReceiverName.Id).Latitude
                        },
                        TransportDistance = getDistance(boDrone.ParcelInDelivery.CollectingPlace, boDrone.ParcelInDelivery.DestinationPlace)
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
        private double getDistance(Location l1, Location l2)
        {
            return idal.DistanceCalculate(l1.Latitude, l1.Latitude, l2.Latitude, l2.Longitude);
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
                Id = item.Id,//id of parcel
                Weight = (IBL.BO.WeightCategories)item.Weight,
                Priority = (IBL.BO.Priorities)item.Priority,
                ParcelState = getParcelState(item.Id),//החבילה נאספה והיא בדרך
                customer = new CustomerOfParcel
                {
                    Id = item.TargetId,
                    Name = idal.GetCustomer(item.TargetId).Name
                },//צריך לאתחל איי די ושם של הלקוח או השולח הפוך מהלוקח המקורי

            };

        }
        private IEnumerable<ParcelAtCustomer> GetParcelsToCustomer(int id)
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
                ParcelState = getParcelState(item.Id),//החבילה נאספה והיא בדרך
                customer = new CustomerOfParcel
                {
                    Id = item.SenderId,
                    Name = idal.GetCustomer(item.SenderId).Name
                },//צריך לאתחל איי די ושם של הלקוח או השולח הפוך מהלוקח המקורי

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
        private CustomerOfParcel getCustomerOfParcel(int id)
        {
            CustomerOfParcel cp = new CustomerOfParcel();
            IDAL.DO.Customer doCast = new IDAL.DO.Customer();
            doCast = idal.GetCustomer(id);
            cp.Name = doCast.Name;
            cp.Id = id;
            return cp;
        }

        public Parcel getParcel(int id)
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
                boParcel.Weight = (WeightCategories)doParcel.Weight;//באיידאל זה מקסימום וויט ואיי בי אל זה סתם וויט בלי מקסימום 
                boParcel.Priority = (Priorities)doParcel.Priority;
                boParcel.ParcelsDrones = GetDroneInParcel(doParcel.DroneId);
                //boParcel.CreatingParcel למה זה שווה בדיוק
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
        private IEnumerable<DroneInParcel> GetDroneInParcel(int id)
        {//עשיתי פונקציה שעושה מה שרצית לעשות פה. אם הבנתי מה רצית
            
             return from item in idal.GetDroneInParcelByPredicate(item => item.Id == id)
             select new DroneInParcel
             {
                Id = id,
                BattaryStatus = ListBLDrones.Find(d => id == item.Id).BatteryStatus,
                Location= ListBLDrones.Find(d => id == item.Id).Location,
             };

        }
    }

}
