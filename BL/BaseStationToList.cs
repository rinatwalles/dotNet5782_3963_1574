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
        class BaseStationToList
        {
            public int id { get; set; }
            public string name { get; set; }
            public int availableCharginggSlotsNumber { get; set; }
            public int reservedCharginggSlotsNumber { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }  
//}
