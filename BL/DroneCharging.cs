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
        class DroneCharging
        {
            public int id { get; set; }
            public int battaryStatus { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
