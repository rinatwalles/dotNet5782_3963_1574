using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Dal.DataSource;
using DaLApi;

namespace Dal
{
    partial class DalXML:IDAL
    {
        /// <summary>
        /// function that gets an id of a parcel and checks if it is in the list
        /// </summary>
        /// <param name="id">id of a parcel</param>
        /// <returns>true if the parcel exist, if not returns false</returns>
        public bool CheckParcel(int id)
        {
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return listParcels.Any(p => p.Id == id);
        }

        /// <summary>
        /// add new parcel
        /// </summary>
        /// <param name="s">new parcel</param>
        public void ParcelAddition(Parcel p)
        {
            p.Id = Config.ParcelId++;
            if (CheckParcel(p.Id))
                throw new DuplicateIdException(p.Id, "Parcel");
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            listParcels.Add(p);
            XMLTools.SaveListToXMLSerializer(listParcels, ParcelsPath);
        }

        /// <summary>
        /// search for parcel according to its id in the list
        /// </summary>
        /// <param name="id">parcel id to search for in the list</param>
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel(id))
                throw new MissingIdException(id, "Parcel");
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return listParcels.Find(p => p.Id == id);
        }

        /// <summary>
        /// function that returns all the parcels in the dal
        /// </summary>
        /// <returns>all the parcels</returns>
        public IEnumerable<Parcel> AllParcel()
        {
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return from parc in listParcels
                   select parc;
        }

        /// <summary>
        /// function that gets a parcel and deletes it from the list
        /// </summary>
        /// <param name="p">the parcel to delete</param>
        public void ParcelDelete(int id)
        {
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            int count = listParcels.RemoveAll(parc => parc.Id == id);
            if (count == 0)
                throw new MissingIdException(id, "Parcel");
            XMLTools.SaveListToXMLSerializer(listParcels, ParcelsPath);
        }

        //public Parcel getParcelByDroneId(int id)
        //{
        //   throw new NotImplementedException();
        //}

        /// <summary>
        /// function that gets a parcel and update it in the list
        /// </summary>
        /// <param name="p">the new parcel</param>
        public void ParcelUpdate(Parcel p)
        {
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            int count = listParcels.RemoveAll(parc => parc.Id == p.Id);
            if (count == 0)
                throw new MissingIdException(p.Id, "Parcel");
            listParcels.Add(p);
            XMLTools.SaveListToXMLSerializer(listParcels, ParcelsPath);
        }

        /// <summary>
        ///  a function that gets a predicate and returns a list of all the parcels with this preducate
        /// </summary>
        /// <param name="predicate">predicate of a parcel</param>
        /// <returns>the parcel</returns>
        public IEnumerable<Parcel> GetParcelByPredicate(Predicate<Parcel> predicate)
        {
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return from par in listParcels
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
            List<Parcel> listParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return listParcels.Find(predicate);
        }

        //public IEnumerable<Drone> GetDroneInParcelByPredicate(Predicate<Drone> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
