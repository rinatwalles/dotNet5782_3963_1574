using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaLApi;


namespace Dal
{
     partial class DalObject : IDAL
    {
        /// <summary>
        /// function that checks if a customer exists in the list or not
        /// </summary>
        /// <param name="id">id f a customer</param>
        /// <returns>returns true if exists if not false</returns>
        public bool CheckCustomer(int id)
        {
            return DataSource.customers.Any(c => c.Id == id);
        }

        /// <summary>
        /// add new customer
        /// </summary>
        /// <param name="c">new customer</param>
        public void CustomerAddition(Customer c)
        {
            if (CheckCustomer(c.Id))
                throw new DuplicateIdException(c.Id, "Customer");
            DataSource.customers.Add(c);
        }

        /// <summary>
        /// search for Customer according to its id in the array
        /// </summary>
        /// <param name="id">Customer id to search for in the array</param>
        /// <returns>return the index of the station in the array</returns>
        public Customer GetCustomer(int id)
        {
            if (!CheckCustomer(id))
                throw new MissingIdException(id, "Customer");
            return DataSource.customers.Find(c => c.Id == id);
        }

        /// <summary>
        /// collecting the parcel cy the costumer
        /// </summary>
        /// <param name="parcelId">parcel id to collect</param>
        public void CustomerCollecting(int parcelId)
        {
            Parcel p = GetParcel(parcelId);
            p.DeliveredTime = DateTime.Now;//change time
        }

        /// <summary>
        /// returning array of all customers
        /// </summary>
        public IEnumerable<Customer> AllCustomer()
        {
            return from custo in DataSource.customers
                   select custo;
        }

        /// <summary>
        /// current distance fron customer
        /// </summary>
        /// <param name="id">costuner id</param>
        /// <param name="x1">current x coordinate</param>
        /// <param name="y1">current y coordinate</param>
        public double DistanceFromCustomer(int id, double long1, double lat1)
        {
            Customer c = GetCustomer(id);
            double long2 = c.Longitude;//custoner coordinates
            double lat2 = c.Latitude;
            return DistanceCalculate(long1, lat1, long2, lat2);//print the distance
        }

        /// <summary>
        /// delete customer function
        /// </summary>
        /// <param name="c">the customer</param>
        public void CustomerDelete(Customer c)
        {
            int count = DataSource.customers.RemoveAll(custo => custo.Id == c.Id);
            if (count == 0)
                throw new MissingIdException(c.Id, "Customer");
        }

        /// <summary>
        /// function that gets a customer and update it in the list
        /// </summary>
        /// <param name="c">the customer</param>
        public void CustomerUpdate(Customer c)
        {
            int count = DataSource.customers.RemoveAll(custo => custo.Id == c.Id);
            if (count == 0)
                throw new MissingIdException(c.Id, "Customer");
            DataSource.customers.Add(c);
        }
        public IEnumerable<Customer> GetCustomerInParcelByPredicate(Predicate<Customer> predicate)
        {
            return from cust in DataSource.customers
                   where predicate(cust)
                   select cust;
        }
    }
}
