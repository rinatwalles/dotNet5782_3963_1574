﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace IBL.BO
    {
        class DroneInParcel
        {
            public int id { get; set; }
            public int battaryStatus { get; set; }
            public Location location { get; set; }
        }
    }
}
