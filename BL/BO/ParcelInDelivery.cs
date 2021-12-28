using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace BL
//{
namespace BO
{
    public class ParcelInDelivery
    {
        public int Id { get; set; }
        public bool ParcelState { get; set; }//true- in delivery false not
        public CustomerOfParcel SenderName { get; set; }
        public CustomerOfParcel ReceiverName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Location CollectingPlace { get; set; }
        public Location DestinationPlace { get; set; }
        public double TransportDistance { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
//}