using DaLApi;
using DO;
using System;
using System.Collections.Generic;

namespace Dal
{
    sealed partial class DalXML : IDAL
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

        
        
    }
}
