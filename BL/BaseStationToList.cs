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
       public class BaseStationToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int AvailableCharginggSlotsNumber { get; set; }
            public int RservedCharginggSlotsNumber { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }  
//}
