using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

//namespace BL
//{
    namespace IBL.BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Location Place { get; set; }
            public List<ParcelAtCustomer> ParcelsFromCustomer { get; set; }
            public List<ParcelAtCustomer> ParcelsToCustomer { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    }
//}
