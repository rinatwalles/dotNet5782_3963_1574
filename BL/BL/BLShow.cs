﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
//the partial class of showing the lists


namespace BL
{
     partial class BL : IBL
    {
        /// <summary>
        /// a function that returns all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> GetAllStations()
        {
            return (from dostat in idal.AllStation()
                   select new StationToList()
                   {
                       Id = dostat.Id,
                       Name = dostat.Name,
                       AvailableChargeSlots = dostat.AvailableChargeSlots,
                       RservedCharginggSlotsNumber = idal.GetDroneChargeInStation(dostat.Id).Count()
                   }).ToList(); ;
        }


        public IEnumerable<StationToList> GetAllStations(Predicate<StationToList> predicate)
        {
            try
            {
                return (from dostat in GetAllStations()
                        where predicate(dostat)
                        select dostat);
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// function that returns all the drones
        /// </summary>
        /// <returns>all the drones in the list</returns>
        public IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> predicate)
        {
            return from bodrone in ListBLDrones
                   where predicate(bodrone)
                   select bodrone;
        }
        public IEnumerable<DroneToList> GetAllDrones()
        {
            return (from bodrone in ListBLDrones
                   select bodrone).ToList() ; ;
        }
        /// <summary>
        /// a function that returns all the parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> GetAllParcels()
        {
            try
            {
                return( from doparc in idal.AllParcel()
                       select new ParcelToList()
                       {
                           Id = doparc.Id,
                           Sender = getCustomerOfParcel(doparc.SenderId),
                           Receiver = getCustomerOfParcel(doparc.TargetId),
                           Weight = (WeightCategories)doparc.Weight,
                           Priority = (Priorities)doparc.Priority,
                           ParcelState = getParcelState(doparc.Id)
                       }).ToList(); ;
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }


        public IEnumerable<ParcelToList> GetAllParcel(Predicate<ParcelToList> predicate)
        {
            try
            {
                return (from doparc in GetAllParcels()
                        where predicate(doparc)
                        select doparc);
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// a private function that returns the num of parcels in a certain state
        /// </summary>
        /// <param name="id"> id of a parcel</param>
        /// <param name="p"> the state of the parcels</param>
        /// <returns></returns>
        private int numParcelsSent(int id, ParcelStates p)
        {
            int num = 0;
            foreach (DO.Parcel item in idal.AllParcel())
            {
                if (getParcelState(item.Id) == p&&item.SenderId==id)
                    num++;
            }
            return num;
        }

        /// <summary>
        /// A function that returns all the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerToList> GetAllCustomers()
        {
            try
            {
                return (from docust in idal.AllCustomer()
                       select new CustomerToList()
                       {
                           Id = docust.Id,
                           Name = docust.Name,
                           Phone = docust.Phone,
                           NumOfParcelsSentAndSupplied = numParcelsSent(docust.Id, ParcelStates.Delivered),
                           NumOfParcelsSentNotSupplied = numParcelsSent(docust.Id, ParcelStates.PickedUp),
                           NumOfParcelsDelivered = GetParcelsFromCustomer(docust.Id).Count(),
                           NumOfParcelsReceived = GetParcelsToCustomer(docust.Id).Count()
                       }).ToList(); ;
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }


        public IEnumerable<CustomerToList> GetAllCustomers(Predicate<CustomerToList> predicate)
        {
            try
            {
                return (from docust in GetAllCustomers()
                        where predicate(docust)
                        select docust) ;
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        ///  A function that returns All Parcels that Not Scheduled
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> GetAllParcelsNotScheduled()//change this function to predicate
        {
            try
            {
                return (from doparc in idal.AllParcel()
                       where (getParcelState(doparc.Id) == ParcelStates.Requested)  //requested but not schedualed
                       select new ParcelToList()
                       {
                           Id = doparc.Id,
                           Sender = getCustomerOfParcel(doparc.SenderId),
                           Receiver = getCustomerOfParcel(doparc.TargetId),
                           Weight = (WeightCategories)doparc.Weight,
                           Priority = (Priorities)doparc.Priority,
                           ParcelState = getParcelState(doparc.Id)
                       }).ToList(); ;
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        /// <summary>
        /// A function that returns All Stations With Available Slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> GetAllStationsWithAvailableSlots()
        {
            return (from dostat in idal.AllStation()
                   where (dostat.AvailableChargeSlots>0)
                   select new StationToList()
                   {
                       Id = dostat.Id,
                       Name = dostat.Name,
                       AvailableChargeSlots = dostat.AvailableChargeSlots,
                       RservedCharginggSlotsNumber = idal.GetDroneChargeInStation(dostat.Id).Count()
                   }).ToList(); ;
        }
    }
}
