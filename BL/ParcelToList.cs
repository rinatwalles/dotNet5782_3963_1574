using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace BL
//{
    namespace IBL.BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }
            public CustomerOfParcel SenderName { get; set; }
            public CustomerOfParcel ReceiverName { get; set; }
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
