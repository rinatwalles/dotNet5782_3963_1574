using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace IBL.BO
    {
        public class Parcel
        { 
            public int Id { get; set; }
            public CustomerOfParcel Sender { get; set; }
            public CustomerOfParcel Receiver { get; set; }

        }
    }
}
