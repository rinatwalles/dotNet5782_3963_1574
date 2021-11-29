using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static DalObject.DataSource;


namespace DalObject
{
    public partial class DalObject : DAL.IDAL.IDAL
    {
        /// <summary>
        /// create initialized arrays
        /// </summary>
        public DalObject() { DataSource.Initialize(); }
        public bool CheckDrone(int id)
        {
            return DataSource.drones.Any(d => d.Id == id);
        }
        public void DroneAddition(Drone d)
        {
            if (!CheckDrone(d.Id))
                throw new DAL.DuplicateIdException(d.Id, "Drone");
            DataSource.drones.Add(d);
        }
        /// <summary>
        /// search drone accroding to its id in the array
        /// </summary>
        /// <param name="id">drone id to search for</param>
        /// <returns>return the index of the drone in the array</returns>
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new DAL.MissingIdException(id, "Drone");
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
            //Parcel p = GetParcel(parcelId);
            //int indexP = SearchParcel(parcelId);//search  for parcel with the given id
            //p.PickedUp = DateTime.Now;//change time
            for (int i = 0; i < parcels.Count; i++)
            {
                if (parcels[i].Id == parcelId)
                {
                    Parcel p = parcels[i];
                    p.PickedUpTime = DateTime.Now;
                    parcels[i] = p;
                }

            }
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
            DataSource.droneCharges.Add(dc);//adding to drone chrge array
                                            // DataSource.config.droneChargeCounter++;

            //Station s = GetStation(stationId);//search station
            //s.ChargeSlots--;//decresing charge slots number
            // DataSource.drones[indexD].Battery = 100;//full battery
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].Id == stationId)
                {
                    Station st = stations[i];
                    st.AvailableChargeSlots--;
                    stations[i] = st;
                }

            }
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
                //Station s = GetStation(stationId);
                //s.ChargeSlots++;//increasing charge number number
                for (int i = 0; i < stations.Count; i++)
                {
                    if (stations[i].Id == stationId)
                    {
                        Station st = stations[i];
                        st.AvailableChargeSlots++;
                        stations[i] = st;
                    }

                }
            }
        }


        /// <summary>
        ///  returning array of all drones
        /// </summary>
        public IEnumerable<Drone> AllDrones()
        {
            return from dr in DataSource.drones
                   select dr;
            //return DataSource.drones;
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
        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
        public void DroneDelete(Drone d)
        {
            int count = DataSource.drones.RemoveAll(dr => dr.Id == d.Id);
            if(count==0)
                throw new DAL.MissingIdException(d.Id, "Drone");
        }

        public void DroneUpdate(Drone d)
        {
            int count = DataSource.drones.RemoveAll(dr => dr.Id == d.Id);
            if (count == 0)
                throw new DAL.MissingIdException(d.Id, "Drone");
            DataSource.drones.Add(d);
        }

       public double[] AskingElectricityUse()
        {
            double[] arr= new double[5];
            arr[0] = DataSource.Config.Availavble;
            arr[1] = DataSource.Config.Light;
            arr[2] = DataSource.Config.Medium;
            arr[3] = DataSource.Config.Heavy;
            arr[4] = DataSource.Config.ChargePrecent;
            return arr;
        }
        public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        {
            return from dr in DataSource.drones
                   where predicate(dr)
                   select dr;
        }
    }
}
