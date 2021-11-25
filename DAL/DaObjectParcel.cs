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
        public bool CheckParcel(int id)
        {
            return DataSource.parcels.Any(p => p.Id == id);
        }
        /// <summary>
        /// add new parcel
        /// </summary>
        /// <param name="s">new parcel</param>
        public void ParcelAddition(Parcel p)
        {
            p.Id = Config.ParcelId++;
            if (!CheckParcel(p.Id))
                throw new DAL.DuplicateIdException(p.Id, "Parcel");
            DataSource.parcels.Add(p);
        }
        /// <summary>
        /// search for parcel according to its id in the array
        /// </summary>
        /// <param name="id">parcel id to search for in the array</param>
        /// <returns>return the index of the costuner in the array</returns>
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel (id))
                throw new DAL.MissingIdException(id, "Parcel");
            return DataSource.parcels.Find(p => p.Id == id);
        }
        public IEnumerable<Parcel> AllParcel()
        {
            //List<Station> newList = new List<Station>();
            //foreach (Station item in DataSource.stations)
            //{
            //    newList.Add(item);
            //}
            //return newList;
            return from parc in DataSource.parcels
                   select parc;
        }
        public void ParcelDelete(Parcel p)
        {
            int count = DataSource.parcels.RemoveAll(parc => parc.Id == p.Id);
            if (count == 0)
                throw new DAL.MissingIdException(p.Id, "Parcel");
        }

        public void ParcelUpdate(Parcel p)
        {
            int count = DataSource.parcels.RemoveAll(parc => parc.Id == p.Id);
            if (count == 0)
                throw new DAL.MissingIdException(p.Id, "Parcel");
            DataSource.parcels.Add(p);
        }

    }
}
