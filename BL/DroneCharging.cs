﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

//namespace BL
//{
    namespace IBL.BO
    {
        public class DroneCharging
        {
            public int Id { get; set; }
            public int BattaryStatus { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}