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
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public ParcelStates ParcelState { get; set; }
            public CustomerOfParcel customer { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}