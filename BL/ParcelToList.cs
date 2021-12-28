using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace BL
//{
    namespace BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }
            public CustomerOfParcel Sender { get; set; }
            public CustomerOfParcel Receiver { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public ParcelStates ParcelState { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }

        }
    }
//}
