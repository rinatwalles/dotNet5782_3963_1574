using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaLApi;

namespace Dal
{
    public partial class DalObject : IDAL
    {
        /// <summary>
        /// function that gets an id of a station and checks if it is in the list
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns>true if the station exist, if not returns false</returns>
        public bool CheckStation(int id)
        {
            return DataSource.stations.Any(s => s.Id == id);
        }

        /// <summary>
        /// add new station
        /// </summary>
        /// <param name="s">new station</param>
        public void StationAddition(Station s)
        {
            if (CheckStation(s.Id))
                throw new DuplicateIdException(s.Id, "Station");
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// search for station according to its id in the list
        /// </summary>
        /// <param name="id">station id to search for in the list</param>
        /// <returns>return the station in the list</returns>
        public Station GetStation(int id)
        {
            if (!CheckStation(id))
                throw new MissingIdException(id, "Station");
            return DataSource.stations.Find(s => s.Id == id);
        }

        /// <summary>
        /// search for station according to predicate in the list
        /// </summary>
        /// <param name="predicate">predicate of station</param>
        /// <returns>station</returns>
        public Station GetStationBypredicate(Predicate<Station> predicate)
        {
            return DataSource.stations.Find(predicate);
        }

        /// <summary>
        /// function that returns all the stations 
        /// </summary>
        /// <returns>all the stations </returns>
        public IEnumerable<Station> AllStation()
        {
            return from stat in DataSource.stations
                   select stat;
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
        /// a function that delete a station
        /// </summary>
        /// <param name="s">the station to delete</param>
        public void StationDelete(Station s)
        {
            int count = DataSource.stations.RemoveAll(stat => stat.Id == s.Id);
            if (count == 0)
                throw new MissingIdException(s.Id, "Station");
        }

        /// <summary>
        /// a function that gets a station and update it
        /// </summary>
        /// <param name="s">the station to update</param>
        public void StationUpdate(Station s)
        {
            int count = DataSource.stations.RemoveAll(stat => stat.Id == s.Id);
            if (count == 0)
                throw new MissingIdException(s.Id, "Station");
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// a function that gets a predicate and returns the stations
        /// </summary>
        /// <param name="predicate">predicate of a station</param>
        /// <returns>the stations</returns>
        public IEnumerable<Station> GetStationByPredicate(Predicate<Station> predicate)
        {
            return from st in DataSource.stations
                   where predicate(st)
                   select st;
        }
    }
}
