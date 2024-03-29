﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int AvailableChargeSlots { get; set; }
        public IEnumerable<DroneCharging> DronesCharging { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
