using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerOfParcel Sender { get; set; }
        public CustomerOfParcel Receiver { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel ParcelDrones { get; set; }
        public DateTime? RequestedTime { get; set; }    //יצירת חבילה
        public DateTime? ScheduledTime { get; set; }   //שיוך
        public DateTime? PickedUpTime { get; set; }   //איסוף
        public DateTime? DeliveredTime { get; set; }   //אספקה
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}

