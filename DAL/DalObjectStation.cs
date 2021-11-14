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
        /// add new station
        /// </summary>
        /// <param name="s">new station</param>
        public void StationAddition(Station s)
        {
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// search for station according to its id in the array
        /// </summary>
        /// <param name="id">station id to search for in the array</param>
        /// <returns>return the index of the station in the array</returns>
        public Station GetStation(int id)
        {
            return DataSource.stations.Find(s => s.Id == id);
        }

        /// <summary>
        /// returning array of all stations
        /// </summary>
        public IEnumerable<Station> AllStation()
        {
            List<Station> newList = new List<Station>();
            foreach (Station item in DataSource.stations)
            {
                newList.Add(item);
            }
            return newList;
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
        public void StationDelete(Station s)
        {
            DataSource.stations.Remove(s);
        }
    }
}
