using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
//using IDAL.DO;
using IDAL;
using BL.IBL.BO;
namespace BL
{
    public class BL
    {
        DAL.IDAL.IDAL dl;

        ////IDAL.DalObject idal = new IDAL.DalObject();
        //DalObject.DalObject dall = new DalObject.DalObject();

        public BL ()
        {
            dl = new DalObject.DalObject();
        }
        public DroneCharging getDrone(int id)
        {
            Drone boDrone = new Drone();
            
            IDAL.DO.Drone doDrone = dl.GetDrone(id);
            boDrone.id = doDrone.Id;
            boDrone.location=doDrone.
        }
    }
}
