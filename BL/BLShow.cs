//using IBL.BO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BLApi;
////the partial class of showing the lists


//namespace BL
//{
//    public partial class BL : BLApi.IBL
//    {
//        /// <summary>
//        /// a function that returns all the stations
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<StationToList> GetAllStations()
//        {
//            return (from dostat in idal.AllStation()
//                   select new StationToList()
//                   {
//                       Id = dostat.Id,
//                       Name = dostat.Name,
//                       AvailableCharginggSlotsNumber = dostat.AvailableChargeSlots,
//                       RservedCharginggSlotsNumber = idal.CountDroneCharge(dostat.Id).Count()
//                   }).ToList(); ;
//        }

//        /// <summary>
//        /// function that returns all the drones
//        /// </summary>
//        /// <returns>all the drones in the list</returns>
//        public IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> predicate)
//        {
//            return from bodrone in ListBLDrones
//                   where predicate(bodrone)
//                   select bodrone;
//        }
//        public IEnumerable<DroneToList> GetAllDrones()
//        {
//            return (from bodrone in ListBLDrones
//                   select bodrone).ToList() ; ;
//        }
//        /// <summary>
//        /// a function that returns all the parcels
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<ParcelToList> GetAllParcels()
//        {
//            try
//            {
//                return( from doparc in idal.AllParcel()
//                       select new ParcelToList()
//                       {
//                           Id = doparc.Id,
//                           Sender = getCustomerOfParcel(doparc.SenderId),
//                           Receiver = getCustomerOfParcel(doparc.TargetId),
//                           Weight = (WeightCategories)doparc.Weight,
//                           Priority = (Priorities)doparc.Priority,
//                           ParcelState = getParcelState(doparc.Id)
//                       }).ToList(); ;
//            }
//            catch (MissingIdException ex)
//            {
//                throw new MissingIdException(ex.ID, ex.EntityName);
//            }
//        }

//        /// <summary>
//        /// a private function that returns the num of parcels in a certain state
//        /// </summary>
//        /// <param name="id"> id of a parcel</param>
//        /// <param name="p"> the state of the parcels</param>
//        /// <returns></returns>
//        private int numParcelsSent(int id, ParcelStates p)
//        {
//            int num = 0;
//            foreach (IDAL.DO.Parcel item in idal.AllParcel())
//            {
//                if (getParcelState(item.Id) == p)
//                    num++;
//            }
//            return num;
//        }

//        /// <summary>
//        /// A function that returns all the customers
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<CustomerToList> GetAllCustomers()
//        {
//            try
//            {
//                return (from docust in idal.AllCustomer()
//                       select new CustomerToList()
//                       {
//                           Id = docust.Id,
//                           Name = docust.Name,
//                           Phone = docust.Phone,
//                           NumOfParcelsSentAndSupplied = numParcelsSent(docust.Id, ParcelStates.Delivered),
//                           NumOfParcelsSentNotSupplied = numParcelsSent(docust.Id, ParcelStates.PickedUp),
//                           NumOfParcelsDelivered = GetParcelsFromCustomer(docust.Id).Count(),
//                           NumOfParcelsReceived = GetParcelsToCustomer(docust.Id).Count()
//                       }).ToList(); ;
//            }
//            catch (MissingIdException ex)
//            {
//                throw new MissingIdException(ex.ID, ex.EntityName);
//            }
//        }

//        /// <summary>
//        ///  A function that returns All Parcels that Not Scheduled
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<ParcelToList> GetAllParcelsNotScheduled()
//        {
//            try
//            {
//                return (from doparc in idal.AllParcel()
//                       where (getParcelState(doparc.Id) == ParcelStates.Requested)  //requested but not schedualed
//                       select new ParcelToList()
//                       {
//                           Id = doparc.Id,
//                           Sender = getCustomerOfParcel(doparc.SenderId),
//                           Receiver = getCustomerOfParcel(doparc.TargetId),
//                           Weight = (WeightCategories)doparc.Weight,
//                           Priority = (Priorities)doparc.Priority,
//                           ParcelState = getParcelState(doparc.Id)
//                       }).ToList(); ;
//            }
//            catch (MissingIdException ex)
//            {
//                throw new MissingIdException(ex.ID, ex.EntityName);
//            }
//        }

//        /// <summary>
//        /// A function that returns All Stations With Available Slots
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<StationToList> GetAllStationsWithAvailableSlots()
//        {
//            return (from dostat in idal.AllStation()
//                   where (dostat.AvailableChargeSlots>0)
//                   select new StationToList()
//                   {
//                       Id = dostat.Id,
//                       Name = dostat.Name,
//                       AvailableCharginggSlotsNumber = dostat.AvailableChargeSlots,
//                       RservedCharginggSlotsNumber = idal.CountDroneCharge(dostat.Id).Count()
//                   }).ToList(); ;
//        }
//    }
//}
