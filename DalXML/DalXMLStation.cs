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
        public bool CheckStation(int id)
        {
            List<Station> listStations =XMLTools.La
            return DataSource.stations.Any(s => s.Id == id);
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
    }
}
