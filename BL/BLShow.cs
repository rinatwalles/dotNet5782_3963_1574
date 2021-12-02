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
        
        //תצגוגת של רשימות
        public IEnumerable<StationToList> GetAllStations()
        {
            return from dostat in idal.AllStation()
                   select new StationToList()
                   {
                       Id = dostat.Id,
                       Name = dostat.Name,
                       AvailableCharginggSlotsNumber = dostat.AvailableChargeSlots,
                       RservedCharginggSlotsNumber = idal.CountDroneCharge(dostat.Id).Count()
                   };
        }
        public IEnumerable<DroneToList> GetAllDrones()
        {
            return from dodrone in ListBLDrones
                   select new DroneToList();
        }

        public IEnumerable<ParcelToList> GetAllParcels()
        {
            try
            {
                return from doparc in idal.AllParcel()
                       select new ParcelToList()
                       {
                           Id = doparc.Id,
                           Sender = getCustomerOfParcel(doparc.SenderId),
                           Receiver = getCustomerOfParcel(doparc.TargetId),
                           Weight = (WeightCategories)doparc.Weight,
                           Priority = (Priorities)doparc.Priority,
                           ParcelState = getParcelState(doparc.Id)
                       };
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        private int numParcelsSent(int id, ParcelStates p)
        {
            int num = 0;
            foreach (IDAL.DO.Parcel item in idal.AllParcel())
            {
                if (getParcelState(item.Id) == p)
                    num++;
            }
            return num;
        }

        public IEnumerable<CustomerToList> GetAllCustomers()
        {
            try
            {
                return from docust in idal.AllCustomer()
                       select new CustomerToList()
                       {
                           Id = docust.Id,
                           Name = docust.Name,
                           Phone = docust.Phone,
                           NumOfParcelsSentAndSupplied = numParcelsSent(docust.Id, ParcelStates.Delivered),
                           NumOfParcelsSentNotSupplied = numParcelsSent(docust.Id, ParcelStates.PickedUp),
                           NumOfParcelsDelivered = GetParcelsFromCustomer(docust.Id).Count(),
                           NumOfParcelsReceived = GetParcelsToCustomer(docust.Id).Count()
                       };
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public IEnumerable<ParcelToList> GetAllParcelsNotScheduled()
        {
            try
            {
                return from doparc in idal.AllParcel()
                       where (getParcelState(doparc.Id) == ParcelStates.Requested)  //requested but not schedualed
                       select new ParcelToList()
                       {
                           Id = doparc.Id,
                           Sender = getCustomerOfParcel(doparc.SenderId),
                           Receiver = getCustomerOfParcel(doparc.TargetId),
                           Weight = (WeightCategories)doparc.Weight,
                           Priority = (Priorities)doparc.Priority,
                           ParcelState = getParcelState(doparc.Id)
                       };
            }
            catch (MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }

        public IEnumerable<StationToList> GetAllStationsWithAvailableSlots()
        {
            return from dostat in idal.AllStation()
                   where (dostat.AvailableChargeSlots>0)
                   select new StationToList()
                   {
                       Id = dostat.Id,
                       Name = dostat.Name,
                       AvailableCharginggSlotsNumber = dostat.AvailableChargeSlots,
                       RservedCharginggSlotsNumber = idal.CountDroneCharge(dostat.Id).Count()
                   };
        }
    }
}
