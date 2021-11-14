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
            //DataSource.stations[DataSource.config.stationCounter] = s;
            //DataSource.config.stationCounter++;
            DataSource.stations.Add(s);
        }

        /// <summary>
        /// search for station according to its id in the array
        /// </summary>
        /// <param name="id">station id to search for in the array</param>
        /// <returns>return the index of the station in the array</returns>
        public Station GetStation(int id)
        {
            //for (int i = 0; i < DataSource.config.droneCounter; i++)
            //{
            //    if (DataSource.stations[i].Id == id)
            //        return i;
            //}
            //return -1;
            return DataSource.stations.Find(s => s.Id == id);
        }

    }
}
