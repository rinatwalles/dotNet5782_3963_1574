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
        Drone getDrone(int id);
        BaseStation getBaseStation(int id);
        Customer GetCustomer(int id);
    }
}
