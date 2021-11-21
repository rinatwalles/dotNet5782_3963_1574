using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IBL.BO;

namespace BL
{
    public class BL : IBL.IBL
    {
        DAL.IDAL.IDAL idal;
        public BL()
        {
            idal = new DalObject.DalObject();
        }


    }
   
}
