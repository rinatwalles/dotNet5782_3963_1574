﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class StationToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargeSlots { get; set; }
        public int RservedCharginggSlotsNumber { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}