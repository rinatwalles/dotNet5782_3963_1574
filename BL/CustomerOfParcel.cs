using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

//namespace BL
//{
    namespace BO
    {
        public class CustomerOfParcel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return this.ToStringProperty();
            }
        }
    }
//}
