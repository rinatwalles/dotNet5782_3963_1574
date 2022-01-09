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
        public bool CheckParcel(int id)
        {
            throw new NotImplementedException();
        }

        public void ParcelAddition(Parcel p)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> AllParcel()
        {
            throw new NotImplementedException();
        }

        public void ParcelDelete(Parcel p)
        {
            throw new NotImplementedException();
        }

        public Parcel getParcelByDroneId(int id)
        {
            throw new NotImplementedException();
        }

        public void ParcelUpdate(Parcel p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelByPredicate(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }
        public Parcel GetOneParcelByPredicate(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
