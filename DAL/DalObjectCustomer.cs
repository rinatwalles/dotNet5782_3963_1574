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
        /// add new customer
        /// </summary>
        /// <param name="c">new customer</param>
        public void CustomerAddition(Customer c)
        {
            //DataSource.customers[DataSource.config.customerCounter] = c;
            //DataSource.config.customerCounter++;
            DataSource.customers.Add(c);
        }
    }
}
