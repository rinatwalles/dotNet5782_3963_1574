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

        public bool CheckCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void CustomerAddition(Customer c)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void CustomerCollecting(int parcelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> AllCustomer()
        {
            throw new NotImplementedException();
        }

        public double DistanceFromCustomer(int id, double x1, double y1)
        {
            throw new NotImplementedException();
        }

        public void CustomerDelete(Customer c)
        {
            throw new NotImplementedException();
        }

        public void CustomerUpdate(Customer c)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerInParcelByPredicate(Predicate<Customer> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
