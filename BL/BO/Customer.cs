﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<ParcelAtCustomer> ParcelsFromCustomer { get; set; }
        public IEnumerable<ParcelAtCustomer> ParcelsToCustomer { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}

