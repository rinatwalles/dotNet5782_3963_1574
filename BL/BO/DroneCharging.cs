using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace BL
//{
namespace BO
{
    public class DroneCharging
    {
        public int Id { get; set; }
        public double BattaryStatus { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
//}
