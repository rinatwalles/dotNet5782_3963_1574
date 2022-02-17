using DaLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
        private readonly string ConfigPath = "Config.xml";

        //<ParcelId> 6 </ParcelId>


        /// <summary>
        /// function that checks if a drone is exists
        /// </summary>
        /// <param name="id">id of a drone</param>
        /// <returns>true if exists</returns>
        public bool CheckDrone(int id)
        {
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            return listDrones.Any(d => d.Id == id);
        }

        /// <summary>
        /// add drone function
        /// </summary>
        /// <param name="d">the drone</param>
        public void DroneAddition(Drone d)
        {
            if (CheckDrone(d.Id))
                throw new DuplicateIdException(d.Id, "Drone");
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            listDrones.Add(d);
            XMLTools.SaveListToXMLSerializer(listDrones, DronesPath);
        }

        /// <summary>
        /// search drone accroding to its id in the array
        /// </summary>
        /// <param name="id">drone id to search for</param>
        /// <returns>return the index of the drone in the array</returns>
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            return listDrones.Find(d => d.Id == id);
        }

        //public void JoinDroneToParcel(int parcelId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DroneCollecting(int parcelId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ChargingDrone(int droneId, int stationId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ReleaseDrone(int droneId, int stationId)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        ///  returning array of all drones
        /// </summary>
        public IEnumerable<Drone> AllDrones()
        {
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            return from dr in listDrones
                   select dr;
        }

        /// <summary>
        /// calaulates distance between coordinates
        /// </summary>
        /// <param name="x1">x coordinate</param>
        /// <param name="y1">y coordinate</param>
        /// <param name="longy">coordinate</param>
        /// <param name="latx">coordinate</param>
        /// <returns></returns>
        public double DistanceCalculate(double long1, double lat1, double long2, double lat2)
        {
            int r = 6371;
            double dLat = deg2rad(lat2 - lat1);
            double dlong = deg2rad(lat2 - lat1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dlong / 2) * Math.Sin(dlong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return r * c;
        }

        /// <summary>
        /// private help function to DistanceCalculate
        /// </summary>
        /// <param name="deg">a degree</param>
        /// <returns></returns>
        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        /// <summary>
        /// function that gets a drone and delete it from the list
        /// </summary>
        /// <param name="d">a drone</param>
        public void DroneDelete(Drone d)
        {
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            int count = listDrones.RemoveAll(dr => dr.Id == d.Id);
            if (count == 0)
                throw new MissingIdException(d.Id, "Drone");
            XMLTools.SaveListToXMLSerializer(listDrones, DronesPath);
        }

        /// <summary>
        /// function that gets a drone , delete it from the list by its id and insert a new  one
        /// </summary>
        /// <param name="d">a drone</param>
        public void DroneUpdate(Drone d)
        {
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            int count = listDrones.RemoveAll(dr => dr.Id == d.Id);
            if (count == 0)
                throw new MissingIdException(d.Id, "Drone");
            listDrones.Add(d);
            XMLTools.SaveListToXMLSerializer(listDrones, DronesPath);

        }

        /// <summary>
        /// a function that returns the data about electricity of drones 
        /// </summary>
        /// <returns>array with electricity data</returns>
        public double[] AskingElectricityUse()
        {
            double[] arr = new double[5];
            XElement ConfigData = XElement.Load(ConfigPath);
            arr[0] = Convert.ToDouble(ConfigData.Element("ElectricityUse").Element("Availavble").Value);
            arr[1] = Convert.ToDouble(ConfigData.Element("ElectricityUse").Element("Light").Value); 
            arr[2] = Convert.ToDouble(ConfigData.Element("ElectricityUse").Element("Medium").Value);
            arr[3] = Convert.ToDouble(ConfigData.Element("ElectricityUse").Element("Heavy").Value);
            arr[4] = Convert.ToDouble(ConfigData.Element("ElectricityUse").Element("ChargePrecent").Value);
            return arr;
        }

        /// <summary>
        /// a function that gets a predicate and returns the drones
        /// </summary>
        /// <param name="predicate">predicate of a drone</param>
        /// <returns>the drones</returns>
        public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        {
            List<Drone> listDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            return from dr in listDrones
                   where predicate(dr)
                   select dr;
        }
        //public string ConvertLatitude(double value)
        //{
        //    return Sexagesimal.ConvertToSexagesimal(value) + 'E';
        //}
        //public string ConvertLongtitude(double value)
        //{
        //    return Sexagesimal.ConvertToSexagesimal(value) + 'S';
        //}
    }
}
