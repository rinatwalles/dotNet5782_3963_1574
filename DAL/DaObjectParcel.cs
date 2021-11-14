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
        /// search for parcel according to its id in the array
        /// </summary>
        /// <param name="id">parcel id to search for in the array</param>
        /// <returns>return the index of the costuner in the array</returns>
        public Parcel GetParcel(int id)
        {
            return DataSource.parcels.Find(p => p.Id == id);
        }
    }
}
