﻿using System;

namespace IDAL
{
    namespace DO
    {
       public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return  $"Id:{Id}\n Model:{Model}\n MaxWeight:{MaxWeight}\n Status:{Status}\n Battery:{Battery}";
            }
        }


    }

}




