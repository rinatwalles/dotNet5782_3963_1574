using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    namespace IDAL
    {
        public interface IDAL
        {
            #region Drone
            //CRUD Drone
            bool CheckDrone(int id);
            void DroneAddition(Drone d);
            Drone GetDrone(int id);
            void JoinDroneToParcel(int parcelId);
            void DroneCollecting(int parcelId);
            void ChargingDrone(int droneId, int stationId);
            void ReleaseDrone(int droneId, int stationId);
            IEnumerable<Drone> AllDrones();
            double DistanceCalculate(double long1, double lat1, double long2, double lat2);
            void DroneDelete(Drone d);
            void DroneUpdate(Drone d);

            double[] AskingElectricityUse();
            IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate);
            #endregion

            #region Customer
            //CRUD Customer
            bool CheckCustomer(int id);
            void CustomerAddition(Customer c);
            Customer GetCustomer(int id);
            void CustomerCollecting(int parcelId);
            IEnumerable<Customer> AllCustomer();
            double DistanceFromCustomer(int id, double x1, double y1);
            void CustomerDelete(Customer c);
            void CustomerUpdate(Customer c);

            #endregion

            #region Station
            //CRUD Station
            bool CheckStation(int id);
            void StationAddition(Station s);
            Station GetStation(int id);
            IEnumerable<Station> AllStation();
            double DistanceFromStation(int id, double x1, double y1);
            void StationDelete(Station s);
            void StationUpdate(Station s);
            IEnumerable<Station> GetStationByPredicate(Predicate<Station> predicate);
            #endregion

            #region Parcel
            //CRUD Parcel
            bool CheckParcel(int id);
            void ParcelAddition(Parcel p);
            Parcel GetParcel(int id);
            IEnumerable<Parcel> AllParcel();
            void ParcelDelete(Parcel p);
            Parcel getParcelByDroneId(int id);
            void ParcelUpdate(Parcel p);
            #endregion

            #region DroneCharge
            //CRUD DroneCharge
            bool CheckDroneCharge(int dId, int sId);
            void DroneChargesDelete(DroneCharge d);
            DroneCharge GetDroneCharge(int dId, int sId);
            IEnumerable<DroneCharge> CountDroneCharge(int id);
            #endregion
        }

    }
}
