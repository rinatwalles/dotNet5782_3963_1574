using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BLApi
{
    public interface IBL
    {
        #region Add
        void AddDrone(Drone ddrone, int sId);
        void AddStation(Station s);
        void AddCustomer(Customer c);
        void AddParcel(Parcel p, int IdSender,  int IdReceiver);
        //Location MinDistanceOfSation(Location locat);

        #endregion

        #region Upadate
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name="", string allChargingPositions="");
        void UpdateCustomer(int id, string name="", string phone="");
        void ReleaseDroneFromCharge(int droneId, TimeSpan t );
        void droneToCharge(int idD);
        void PickedUpParcelByDrone(int droneId);
        void joinParcelToDrone(int idD);
        void supplyParceByDrone(int idD);
        //IDAL.DO.Station minStationDistance(Drone boDrone);
        //Location minStationDistance(Location location);

        #endregion

        #region Show
        IEnumerable<StationToList> GetAllStations();
        IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> predicate);
        IEnumerable<DroneToList> GetAllDrones();
        IEnumerable<CustomerToList> GetAllCustomers();
        IEnumerable<ParcelToList> GetAllParcels();
        IEnumerable<ParcelToList> GetAllParcelsNotScheduled();
        IEnumerable<StationToList> GetAllStationsWithAvailableSlots();
        //int numParcelsSentNotSupplied();
        #endregion

        #region Display
        //CustomerOfParcel getCustomerOfParcel(int id);
        Drone GetDrone(int id);
        Station GetStation(int id);
        Customer GetCustomer(int id);
        Parcel GetParcel(int id);
        //double getDistance(Location l1, Location l2);
        //ParcelStates getParcelState(int id);
        //IEnumerable<ParcelAtCustomer> GetParcelsFromCustomer(int id);
        //IEnumerable<ParcelAtCustomer> GetParcelsToCustomer(int id);
        //IEnumerable<DroneCharging> GetDroneChargingPerStation(int id);
        //IEnumerable<DroneInParcel> GetDroneInParcel(int id);

        #endregion
    }
}
