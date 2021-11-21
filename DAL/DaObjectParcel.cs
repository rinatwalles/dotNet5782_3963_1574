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
            if (CheckParcel(p.Id))
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
            if (CheckParcel (id))
                throw new DAL.MissingIdException(id, "Parcel");
            return DataSource.parcels.Find(p => p.Id == id);
        }
        public void ParcelDelete(Parcel p)
        {
            if (CheckParcel(p.Id))
                throw new DAL.MissingIdException(p.Id, "Parcel");
            DataSource.parcels.Remove(p);
        }

    }
}
