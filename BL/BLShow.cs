using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BLShow : IBL.IBL
    {
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
    }
}
