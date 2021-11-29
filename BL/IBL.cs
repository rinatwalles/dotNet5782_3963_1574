using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        //public BO.ParcelToList GetStudent(int id);

        // public IEnumerable<BO.ParcelToList> GetAllStudents();
        //CRUD location
        #region Location
        #endregion
        void AddDrone(Drone ddrone, int sId);
        void AddBaseStation(BaseStation s);
        void AddCustomer(Customer c);
        void AddParcel(Parcel p, int IdSender,  int IdReceiver);

        void UpdateDrone(int id, string model);
        void UpdateCustomer(int id, string name="", string phone="");


        IEnumerable<BaseStationToList> GetAllBaseStations();
        IEnumerable<DroneToList> GetAllDrones();
        IEnumerable<CustomerToList> GetAllCustomers();


        CustomerOfParcel getCustomerOfParcel(int id);

        Drone getDrone(int id);
        BaseStation getBaseStation(int id);
        Customer GetCustomer(int id);
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
}
