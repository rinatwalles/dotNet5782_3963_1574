using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace IBL.BO
    {
        public interface IBL
        {
            BO.ParcelToList GetStudent(int id);

            IEnumerable<BO.ParcelToList> GetAllStudents();
            //CRUD location
            #region Location
            #endregion
        }
    }
}
