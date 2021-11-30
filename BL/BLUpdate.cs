using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        
        public void UpdateDrone(int id, string model)
        {//מסתמכים שאם הוא נמצר בDAL כבר הוספנו אותו לBL. לא להסתמך על זה?
            try
            {//שינוי הרחפן בDAL
                IDAL.DO.Drone doDrone = new IDAL.DO.Drone();
                doDrone = idal.GetDrone(id);   //הצבה בין שדות?
                //IDAL.DO.Drone doDrone = idal.GetDrone(id);     השורה הזאת יכולה להחליף את 145, 146? כי יכול להיות שמאחורי הקלעים יש new
                doDrone.Model = model;
                idal.DroneUpdate(doDrone);

                //שינוי רחפן ברישמת רחפנים BL
                DroneToList dl = ListBLDrones.Find(d => d.Id == id);        //יצרנו העתק
                int count = ListBLDrones.RemoveAll(dr => dr.Id == id); //מוחקים את הישן
                dl.Model = model;  // changing the model name
                ListBLDrones.Add(dl); //adding the new one
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void UpdateCustomer(int id, string name, string phone)
        {
            try
            {
                IDAL.DO.Customer doCustomer = new IDAL.DO.Customer();
                doCustomer = idal.GetCustomer(id);
                if (name != "")                  //בדיקה אם הוא הכניב ערכים או ENTER
                    doCustomer.Name = name;
                if (phone != "")
                    doCustomer.Phone = phone;
                idal.CustomerUpdate(doCustomer);
            }
            catch (DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }
        }
        public void droneToCharge (int id)
        {
            try 
            {
                IBL.BO.Drone boDrone = GetDrone(id);
                if (boDrone.DroneStatus != DroneStatuses.Available)//אם הרחפן לא פנוי פשוט לצאת??
                    return;
                Location sLocation = new Location { };
                double calculate;
                foreach (IDAL.DO.Station item in idal.GetStationByPredicate(st=>st.AvailableChargeSlots>0))
                {
                    sLocation.Latitude = item.Latitude;
                    sLocation.Longitude = item.Longitude;
                    calculate = getDistance(boDrone.Location, sLocation);
                    if()
                }

                //    
                //
                //calculate += idal.DistanceCalculate(cust.Longitude, cust.Latitude, closeStation.Longitude, closeStation.Latitude) * array[1 + (int)item.Weight];

            }
           catch(DAL.MissingIdException ex)
            {
                throw new MissingIdException(ex.ID, ex.EntityName);
            }

        }
    }
}
