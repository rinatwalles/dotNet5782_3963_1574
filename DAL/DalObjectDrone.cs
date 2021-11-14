using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static DalObject.DataSource;


namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// create initialized arrays
        /// </summary>
        public DalObject() { DataSource.Initialize(); }
        public void DroneAddition(Drone d)
        {
            //DataSource.drones[DataSource.config.droneCounter] = d;
            //DataSource.config.droneCounter++;
            DataSource.drones.Add(d);
        }

        /// <summary>
        /// search drone accroding to its id in the array
        /// </summary>
        /// <param name="id">drone id to search for</param>
        /// <returns>return the index of the drone in the array</returns>
        public Drone GetDrone(int id)
        {
            return DataSource.drones.Find(d => d.Id == id);
        }

        /// <summary>
        /// join drone to parcel
        /// </summary>
        /// <param name="parcelId">parcel id to connect to drone</param>
        public void JoinDroneToParcel(int parcelId)
        {
            Parcel p = GetParcel(parcelId);
           // int index = SearchParcel(parcelId);//search the parcel with the requested id
           // for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
               // if (DataSource.drones[i].Status == DroneStatuses.Available)//search for avilable drone
                {
                  //  DataSource.drones[i].Status = DroneStatuses.Delivery;//change the drone status
                    //p.DroneId = DataSource.drones.Id;//parcel drone id= avilable id    לשנות!!!!!
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
            Parcel p = GetParcel(parcelId);
            //int indexP = SearchParcel(parcelId);//search  for parcel with the given id
            p.PickedUp = DateTime.Now;//change time
        }

        /// <summary>
        /// collecting the parcel cy the costumer
        /// </summary>
        /// <param name="parcelId">parcel id to collect</param>
        public void CustomerCollecting(int parcelId)
        {
            Parcel p = GetParcel(parcelId);
           // int indexP = SearchParcel(parcelId);//search  for parcel with the given id
            p.Delivered = DateTime.Now;//change time
        }

        /// <summary>
        /// charging the drone
        /// </summary>
        /// <param name="droneId">drone id to charge</param>
        /// <param name="stationId">station id to charge in</param>
        public void ChargingDrone(int droneId, int stationId)
        {
            Drone d = GetDrone(droneId);//search drone
            //DataSource.drones[indexD].Status = DroneStatuses.Maintenance;//change drone status

            DroneCharge dc = new DroneCharge() { DroneId = droneId, StationId = stationId };//new drone charge
            DataSource.droneCharges.Add (dc);//adding to drone chrge array
           // DataSource.config.droneChargeCounter++;

            Station s = GetStation(stationId);//search station
            s.ChargeSlots--;//decresing charge slots number
           // DataSource.drones[indexD].Battery = 100;//full battery
        }
        /// <summary>
        /// releasing drone from charging 
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ReleaseDrone(int droneId, int stationId)
        {
            Drone d = GetDrone(droneId);
            //if (d.Battery == 100)//battery is full
            {
                //d.Status = DroneStatuses.Available;//change status
                Station s = GetStation(stationId);
               s.ChargeSlots++;//increasing charge number number
            }
        }

        /// <summary>
        /// print customer by id
        /// </summary>
        /// <param name="newId">costumer id to print</param>
        //public Customer PrintCustomer(int newId)
        //{
        //    int temp = SearchCustomer(newId);
        //    return DataSource.customers[temp];
        //}
        /// <summary>
        /// returning array of all customers
        /// </summary>
        public IEnumerable<Customer> AllCustomer()
        {
            return DataSource.customers;
        }
        /// <summary>
        /// print parcel by id
        /// </summary>
        /// <param name="newId">parcel id to print</param>
        //public Parcel PrintParcel(int newId)
        //{
        //    int temp = SearchParcel(newId);
        //    return DataSource.parcels[temp];
        //}
        /// <summary>
        ///  returning array of all parcels
        /// </summary>
        public IEnumerable<Parcel> AllParcels()
        {
            return DataSource.parcels;
        }
        /// <summary>
        /// print station by id
        /// </summary>
        /// <param name="newId">station id to print</param>
        //public Station PrintStation(int newId)
        //{
        //    int temp = SearchStation(newId);
        //    return DataSource.stations[temp];
        //}

        /// <summary>
        /// returning array of all stations
        /// </summary>
        public IEnumerable<Station> AllStation()
        {
            List<Station> newList = new List<Station>(); 
            foreach(Station item in DataSource.stations )
            {
                newList.Add(item);
            }
            return newList;
        }

        /// <summary>
        /// print drone by id   
        /// </summary>
        /// <param name="newId">drone id to print</param>
        //public Drone GetDrone(int newId)
        //{
        //    int temp = SearchDrone(newId);
        //    return  DataSource.drones.Find(Drone.Id==newId);
        //}
        /// <summary>
        ///  returning array of all drones
        /// </summary>
        public IEnumerable<Drone> AllDrones()
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
            Station s = GetStation(id);
            double longy = s.Longitude;//station coordinates
            double latx = s.Latitude;
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
            Customer c = GetCustomer(id);
            double longy = c.Longitude;//custoner coordinates
            double latx = c.Latitude;
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