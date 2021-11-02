using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static DalObject.DataSource;


namespace DalObject
{
    public class DalObject
    {
        /// <summary>
        /// create initialized arrays
        /// </summary>
        public DalObject() { DataSource.Initialize(); }
        public void DroneAddition(Drone d)
        {
            DataSource.drones[DataSource.config.droneCounter] = d;
            DataSource.config.droneCounter++;
        }
        /// <summary>
        /// add new station
        /// </summary>
        /// <param name="s">new station</param>
        public void StationAddition(Station s)
        {
            DataSource.stations[DataSource.config.stationCounter] = s;
            DataSource.config.stationCounter++;
        }
        /// <summary>
        /// add new customer
        /// </summary>
        /// <param name="c">new customer</param>
        public void CustomerAddition(Customer c)
        {
            DataSource.customers[DataSource.config.customerCounter] = c;
            DataSource.config.customerCounter++;
        }
        /// <summary>
        /// add new parcel
        /// </summary>
        /// <param name="p">new parcel</param>
        public void ParcelAddition(Parcel p)
        {
            DataSource.parcels[DataSource.config.parcelsCounter] = p;
            DataSource.config.parcelsCounter++;
        }
        /// <summary>
        /// search drone accroding to its id in the array
        /// </summary>
        /// <param name="id">drone id to search for</param>
        /// <returns>return the index of the drone in the array</returns>
        public int SearchDrone(int id)
        {
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// search for station according to its id in the array
        /// </summary>
        /// <param name="id">station id to search for in the array</param>
        /// <returns>return the index of the station in the array</returns>
        public int SearchStation(int id)
        {
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// search for customer according to its id in the array
        /// </summary>
        /// <param name="newId">costuner id to search for in the array</param>
        /// <returns>return the index of the costuner in the array</returns>
        public int SearchCustomer(int newId)
        {
            for (int i = 0; i < DataSource.config.customerCounter; i++)
            {
                if (DataSource.customers[i].Id == newId)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// search for parcel according to its id in the array
        /// </summary>
        /// <param name="newId">parcel id to search for in the array</param>
        /// <returns>return the index of the costuner in the array</returns>
        public int SearchParcel(int newId)
        {
            for (int i = 0; i < DataSource.config.parcelsCounter; i++)
            {
                if (DataSource.parcels[i].Id == newId)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// join drone to parcel
        /// </summary>
        /// <param name="parcelId">parcel id to connect to drone</param>
        public void JoinDroneToParcel(int parcelId)
        {
            int index = SearchParcel(parcelId);//search the parcel with the requested id
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.drones[i].Status == DroneStatuses.Available)//search for avilable drone
                {
                    DataSource.drones[i].Status = DroneStatuses.Delivery;//change the drone status
                    DataSource.parcels[index].DroneId = DataSource.drones[i].Id;//parcel drone id= avilable id
                    return;
                }
            }
        }
        /// <summary>
        /// collecting the parcel by drone
        /// </summary>
        /// <param name="parcelId">parcel id to collect</param>
        public void DroneCollecting(int parcelId)
        {
            int indexP = SearchParcel(parcelId);//search  for parcel with the given id
            DataSource.parcels[indexP].PickedUp = DateTime.Now;//change time
        }

        /// <summary>
        /// collecting the parcel cy the costumer
        /// </summary>
        /// <param name="parcelId">parcel id to collect</param>
        public void CustomerCollecting(int parcelId)
        {
            int indexP = SearchParcel(parcelId);//search  for parcel with the given id
            DataSource.parcels[indexP].Delivered = DateTime.Now;//change time
        }

        /// <summary>
        /// charging the drone
        /// </summary>
        /// <param name="droneId">drone id to charge</param>
        /// <param name="stationId">station id to charge in</param>
        public void ChargingDrone(int droneId, int stationId)
        {
            int indexD = SearchDrone(droneId);//search drone
            DataSource.drones[indexD].Status = DroneStatuses.Maintenance;//change drone status

            DroneCharge dc = new DroneCharge() { DroneId = droneId, StationId = stationId };//new drone charge
            DataSource.droneCharges[DataSource.config.droneChargeCounter] = dc;//adding to drone chrge array
            DataSource.config.droneChargeCounter++;

            int indexS = SearchStation(stationId);//search station
            DataSource.stations[indexS].ChargeSlots--;//decresing charge slots number
            DataSource.drones[indexD].Battery = 100;//full battery
        }
        /// <summary>
        /// releasing drone from charging 
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ReleaseDrone(int droneId, int stationId)
        {
            int indexD = SearchDrone(droneId);
            if (DataSource.drones[indexD].Battery == 100)//battery is full
            {
                DataSource.drones[indexD].Status = DroneStatuses.Available;//change status
                DataSource.config.droneChargeCounter--;

                int indexS = SearchDrone(stationId);
                DataSource.stations[indexS].ChargeSlots++;//increasing charge number number
            }
        }

        /// <summary>
        /// print customer by id
        /// </summary>
        /// <param name="newId">costumer id to print</param>
        public Customer PrintCustomer(int newId)
        {
            int temp = SearchCustomer(newId);
            return DataSource.customers[temp];
        }
        /// <summary>
        /// returning array of all customers
        /// </summary>
        public Customer[] AllCustomer()
        {
            return DataSource.customers;
        }
        /// <summary>
        /// print parcel by id
        /// </summary>
        /// <param name="newId">parcel id to print</param>
        public Parcel PrintParcel(int newId)
        {
            int temp = SearchParcel(newId);
            return DataSource.parcels[temp];
        }
        /// <summary>
        ///  returning array of all parcels
        /// </summary>
        public Parcel[] AllParcels()
        {
            return DataSource.parcels;
        }
        /// <summary>
        /// print station by id
        /// </summary>
        /// <param name="newId">station id to print</param>
        public Station PrintStation(int newId)
        {
            int temp = SearchStation(newId);
            return DataSource.stations[temp];
        }

        /// <summary>
        /// returning array of all stations
        /// </summary>
        public Station[] AllStation()
        {
            return DataSource.stations;
        }

        /// <summary>
        /// print drone by id   
        /// </summary>
        /// <param name="newId">drone id to print</param>
        public Drone PrintDrone(int newId)
        {
            int temp = SearchDrone(newId);
            return DataSource.drones[temp];
        }
        /// <summary>
        ///  returning array of all drones
        /// </summary>
        public Drone[] AllDrones()
        {
            return DataSource.drones;
        }
        /// <summary>
        /// current distance fron station
        /// </summary>
        /// <param name="id">station id</param>
        /// <param name="x1">current x coordinate</param>
        /// <param name="y1">current y coordinate</param>
        public double DistanceFromStation(int id, double x1, double y1)
        {
            int temp = SearchStation(id);
            double longy = DataSource.stations[temp].Longitude;//station coordinates
            double latx = DataSource.stations[temp].Latitude;
            return DistanceCalculate(x1, y1, longy, latx);//print the distance
        }
        /// <summary>
        /// current distance fron customer
        /// </summary>
        /// <param name="id">costuner id</param>
        /// <param name="x1">current x coordinate</param>
        /// <param name="y1">current y coordinate</param>
        public double DistanceFromCustomer(int id, double x1, double y1)
        {
            int temp = SearchCustomer(id);
            double longy = DataSource.customers[temp].Longitude;//custoner coordinates
            double latx = DataSource.customers[temp].Latitude;
            return DistanceCalculate(x1, y1, longy, latx);//print the distance
        }
        /// <summary>
        /// calaulates distance between coordinates
        /// </summary>
        /// <param name="x1">x coordinate</param>
        /// <param name="y1">y coordinate</param>
        /// <param name="longy">coordinate</param>
        /// <param name="latx">coordinate</param>
        /// <returns></returns>
        public double DistanceCalculate(double x1, double y1, double longy, double latx)
        {
            return (Math.Sqrt(Math.Pow(x1-latx, 2)+Math.Pow(y1-longy,2)));//distance calculation
        }
    };
}