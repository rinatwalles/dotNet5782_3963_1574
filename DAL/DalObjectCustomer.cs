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
        /// add new customer
        /// </summary>
        /// <param name="c">new customer</param>
        public void CustomerAddition(Customer c)
        {
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// search for Customer according to its id in the array
        /// </summary>
        /// <param name="id">Customer id to search for in the array</param>
        /// <returns>return the index of the station in the array</returns>
        public Customer GetCustomer(int id)
        {
            return DataSource.customers.Find(c => c.Id == id);
        }
        /// <summary>
        /// collecting the parcel cy the costumer
        /// </summary>
        /// <param name="parcelId">parcel id to collect</param>
        public void CustomerCollecting(int parcelId)
        {
            Parcel p = GetParcel(parcelId);
            p.Delivered = DateTime.Now;//change time
        }
        /// <summary>
        /// returning array of all customers
        /// </summary>
        public IEnumerable<Customer> AllCustomer()
        {
            return DataSource.customers;
        }
        /// <summary>
        /// current distance fron customer
        /// </summary>
        /// <param name="id">costuner id</param>
        /// <param name="x1">current x coordinate</param>
        /// <param name="y1">current y coordinate</param>
        public double DistanceFromCustomer(int id, double x1, double y1)
        {
            Customer c = GetCustomer(id);
            double longy = c.Longitude;//custoner coordinates
            double latx = c.Latitude;
            return DistanceCalculate(x1, y1, longy, latx);//print the distance
        }
        public void CustomerDelete(Customer c)
        {
            DataSource.customers.Remove(c);
        }
    }
}
