using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        #region Add
        void AddDrone(Drone ddrone, int sId);
        void AddStation(Station s);
        void AddCustomer(Customer c);
        void AddParcel(Parcel p, int IdSender,  int IdReceiver);
        Location MinDistanceOfSation(Location locat);

        #endregion

        #region Upadate
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name="", string allChargingPositions="");
        void UpdateCustomer(int id, string name="", string phone="");
        void ReleaseDroneFromCharge(int droneId, TimeSpan t );
        void droneToCharge(int idD);
        void PickedUpParcelByDrone(int droneId);

        #endregion

        #region Show
        IEnumerable<StationToList> GetAllStations();
        IEnumerable<DroneToList> GetAllDrones();
        IEnumerable<CustomerToList> GetAllCustomers();
        IEnumerable<ParcelToList> GetAllParcels();
        IEnumerable<ParcelToList> GetAllParcelsNotScheduled();
        IEnumerable<StationToList> GetAllStationsWithAvailableSlots();
        #endregion

        #region Display
        CustomerOfParcel getCustomerOfParcel(int id);
        Drone getDrone(int id);
        Station getBaseStation(int id);
        Customer GetCustomer(int id);
        Parcel getParcel(int id);
        
        #endregion
    }
}
