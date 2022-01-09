using DaLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    partial class DalXML:IDAL
    {
        /// <summary>
        /// function that gets an id of a station and checks if it is in the list
        /// </summary>
        /// <param name="id">id of a station</param>
        /// <returns>true if the station exist, if not returns false</returns>
        public bool CheckStation(int id)
        {
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer < Station > (StationsPath);
            return listStations.Any(s => s.Id == id);
        }

        /// <summary>
        /// add new station
        /// </summary>
        /// <param name="s">new station</param>
        public void StationAddition(Station s)
        {
            if (CheckStation(s.Id))
                throw new DuplicateIdException(s.Id, "Station");
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            listStations.Add(s);
            XMLTools.SaveListToXMLSerializer(listStations, StationsPath);
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
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return listStations.Find(s => s.Id == id);
        }

        /// <summary>
        /// function that returns all the stations 
        /// </summary>
        /// <returns>all the stations </returns>
        public IEnumerable<Station> AllStation()
        {
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return from stat in listStations
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
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            int count = listStations.RemoveAll(stat => stat.Id == s.Id);

            if (count == 0)
                throw new MissingIdException(s.Id, "Station");
            XMLTools.SaveListToXMLSerializer(listStations, StationsPath);

        }

        /// <summary>
        /// a function that gets a station and update it
        /// </summary>
        /// <param name="s">the station to update</param>
        public void StationUpdate(Station s)
        {
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            int count = listStations.RemoveAll(stat => stat.Id == s.Id);
            if (count == 0)
               throw new MissingIdException(s.Id, "Station");
            listStations.Add(s);
            XMLTools.SaveListToXMLSerializer(listStations, StationsPath);
        }

        /// <summary>
        /// a function that gets a predicate and returns the stations
        /// </summary>
        /// <param name="predicate">predicate of a station</param>
        /// <returns>the stations</returns>
        public IEnumerable<Station> GetStationByPredicate(Predicate<Station> predicate)
        {
            List<Station> listStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return from st in listStations
                   where predicate(st)
                   select st;
        }
    }
}
