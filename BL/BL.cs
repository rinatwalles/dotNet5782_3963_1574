using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DAL.IDAL;

namespace BL
{
    namespace IBL.BO
    {

        public class BL : IBL
        {
            DAL.IDAL.IDAL idal;
            public BL()
            {
                 idal = new DalObject.DalObject();
            }
            // DalObject.DalObject dall = new DalObject.DalObject();


        }
    }
}
