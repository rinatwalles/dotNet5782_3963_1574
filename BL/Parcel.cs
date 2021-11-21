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
        public class Parcel
        { 
            public int Id { get; set; }
            public CustomerOfParcel Sender { get; set; }
            public CustomerOfParcel Receiver { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public IEnumerable<DroneInParcel> ParcelsDrones { get; set; }
            public DateTime CreatingParcel { get; set; }
            public DateTime Requested { get; set; }    //יצירת חבילה
            public DateTime Scheduled { get; set; }   //שיוך
            public DateTime PickedUp { get; set; }   //איסוף
            public DateTime Delivered { get; set; }   //אספקה
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
