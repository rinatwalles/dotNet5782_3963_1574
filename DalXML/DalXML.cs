using DaLApi;
using DO;
using System;
using System.Collections.Generic;

namespace Dal
{
    sealed class DalXML : IDAL
    {
        static readonly IDAL instance = new DalXML();
        public static IDAL Instance { get => instance; }
        DalXML() { }
        private readonly string CustomersPath = "Customers.xml";
        private readonly string ParcelsPath = "Parcels.xml";
        private readonly string DronesPath = "Drones.xml";
        private readonly string StationsPath = "Stations.xml";
        private readonly string DroneChargesPath = "DroneCharges.xml";

        public bool CheckDrone(int id)
        {
            throw new NotImplementedException();
        }

        public void DroneAddition(Drone d)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        public void JoinDroneToParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void DroneCollecting(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void ChargingDrone(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDrone(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> AllDrones()
        {
            throw new NotImplementedException();
        }

        public double DistanceCalculate(double long1, double lat1, double long2, double lat2)
        {
            throw new NotImplementedException();
        }

        public void DroneDelete(Drone d)
        {
            throw new NotImplementedException();
        }

        public void DroneUpdate(Drone d)
        {
            throw new NotImplementedException();
        }

        public double[] AskingElectricityUse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        {
            throw new NotImplementedException();
        }

        public bool CheckCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void CustomerAddition(Customer c)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void CustomerCollecting(int parcelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> AllCustomer()
        {
            throw new NotImplementedException();
        }

        public double DistanceFromCustomer(int id, double x1, double y1)
        {
            throw new NotImplementedException();
        }

        public void CustomerDelete(Customer c)
        {
            throw new NotImplementedException();
        }

        public void CustomerUpdate(Customer c)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerInParcelByPredicate(Predicate<Customer> predicate)
        {
            throw new NotImplementedException();
        }

        public bool CheckStation(int id)
        {
            throw new NotImplementedException();
        }

        public void StationAddition(Station s)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> AllStation()
        {
            throw new NotImplementedException();
        }

        public double DistanceFromStation(int id, double x1, double y1)
        {
            throw new NotImplementedException();
        }

        public void StationDelete(Station s)
        {
            throw new NotImplementedException();
        }

        public void StationUpdate(Station s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStationByPredicate(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public bool CheckParcel(int id)
        {
            throw new NotImplementedException();
        }

        public void ParcelAddition(Parcel p)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> AllParcel()
        {
            throw new NotImplementedException();
        }

        public void ParcelDelete(Parcel p)
        {
            throw new NotImplementedException();
        }

        public Parcel getParcelByDroneId(int id)
        {
            throw new NotImplementedException();
        }

        public void ParcelUpdate(Parcel p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelByPredicate(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }

        public void DroneChargeAddition(DroneCharge dc)
        {
            throw new NotImplementedException();
        }

        public bool CheckDroneCharge(int dId)
        {
            throw new NotImplementedException();
        }

        public void DroneChargesDelete(DroneCharge d)
        {
            throw new NotImplementedException();
        }

        public DroneCharge GetDroneCharge(int dId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneChargeInStation(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel GetOneParcelByPredicate(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
