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
            public int DroneId { get; set; }
            public DateTime RequestedTime { get; set; }   // זמן יצירת חבילה למשלוח
            public DateTime ScheduledTime { get; set; }  // זמן שיוך החבילה לרחפן 
            public DateTime PickedUpTime { get; set; }   // זמן איסוף חבילה מהשולח
            public DateTime DeliveredTime { get; set; }  //זמן הגעת החבילה למקבל

            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
}