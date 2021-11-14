using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }   // זמן יצירת חבילה למשלוח
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }  // זמן שיוך החבילה לרחפן 
            public DateTime PickedUp { get; set; }   // זמן איסוף חבילה מהשולח
            public DateTime Delivered { get; set; }  //זמן הגעת החבילה למקבל

            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}