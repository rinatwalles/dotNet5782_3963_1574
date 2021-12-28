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
        /// function that gets an id of a parcel and checks if it is in the list
        /// </summary>
        /// <param name="id">id of a parcel</param>
        /// <returns>true if the parcel exist, if not returns false</returns>
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
            if (CheckParcel(p.Id))
                throw new DAL.DuplicateIdException(p.Id, "Parcel");
            DataSource.parcels.Add(p);
        }

        /// <summary>
        /// search for parcel according to its id in the list
        /// </summary>
        /// <param name="id">parcel id to search for in the list</param>
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel (id))
                throw new DAL.MissingIdException(id, "Parcel");
            return DataSource.parcels.Find(p => p.Id == id);
        }

        /// <summary>
        /// function that returns all the parcels in the dal
        /// </summary>
        /// <returns>all the parcels</returns>
        public IEnumerable<Parcel> AllParcel()
        {
            return from parc in DataSource.parcels
                   select parc;
        }

        /// <summary>
        /// function that gets a parcel and deletes it from the list
        /// </summary>
        /// <param name="p">the parcel to delete</param>
        public void ParcelDelete(Parcel p)
        {
            int count = DataSource.parcels.RemoveAll(parc => parc.Id == p.Id);
            if (count == 0)
                throw new DAL.MissingIdException(p.Id, "Parcel");
        }

        /// <summary>
        /// function that gets a parcel and update it in the list
        /// </summary>
        /// <param name="p">the new parcel</param>
        public void ParcelUpdate(Parcel p)
        {
            int count = DataSource.parcels.RemoveAll(parc => parc.Id == p.Id);
            if (count == 0)
                throw new DAL.MissingIdException(p.Id, "Parcel");
            DataSource.parcels.Add(p);
        }

        /// <summary>
        /// a function that gets an id and returns its parcel
        /// </summary>
        /// <param name="id">id of a parcel</param>
        /// <returns>a parcel</returns>
        public Parcel getParcelByDroneId (int id)
        {
            if (!CheckParcel(id))
                throw new DAL.MissingIdException(id, "Parcel");
            return DataSource.parcels.Find(p => p.DroneId == id);
        }

        /// <summary>
        ///  a function that gets a predicate and returns a list of all the parcels with this preducate
        /// </summary>
        /// <param name="predicate">predicate of a parcel</param>
        /// <returns>the parcel</returns>
        public IEnumerable<Parcel> GetParcelByPredicate(Predicate<Parcel> predicate)
        {
            return from par in DataSource.parcels
                   where predicate(par)
                   select par;
        }

        /// <summary>
        /// a function that gets a predicate and returns its parcel
        /// </summary>
        /// <param name="predicate">predicate of a parcel</param>
        /// <returns>the parcel</returns>
        public Parcel GetOneParcelByPredicate(Predicate<Parcel> predicate)
        { 
           return DataSource.parcels.Find(predicate);
        }
    }
}
