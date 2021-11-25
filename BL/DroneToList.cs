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
       public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public Location Location { get; set; }
            public int PassedParcelNumber { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}