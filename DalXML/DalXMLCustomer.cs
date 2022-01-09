using DaLApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXML:IDAL
    {

        public bool CheckCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void CustomerAddition(Customer c)
        {
            throw new NotImplementedException();
        }
        //public DO.Person GetPerson(int id)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    Person p = (from per in personsRootElem.Elements()
        //                where int.Parse(per.Element("ID").Value) == id
        //                select new Person()
        //                {
        //                    ID = Int32.Parse(per.Element("ID").Value),
        //                    Name = per.Element("Name").Value,
        //                    Street = per.Element("Street").Value,
        //                    HouseNumber = Int32.Parse(per.Element("HouseNumber").Value),
        //                    City = per.Element("City").Value,
        //                    BirthDate = DateTime.Parse(per.Element("BirthDate").Value),
        //                    PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), per.Element("PersonalStatus").Value),
        //                    Duration = TimeSpan.ParseExact(per.Element("Duration").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
        //                }
        //                ).FirstOrDefault();

        //    if (p == null)
        //        throw new DO.BadPersonIdException(id, $"bad person id: {id}");

        //    return p;
        //}
        public Customer GetCustomer(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
        }

        public void CustomerCollecting(int parcelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> AllCustomer()
        {
            throw new NotImplementedException();
        }

        public double DistanceFromCustomer(int id, double x1, double y1)
        {
            throw new NotImplementedException();
        }

        public void CustomerDelete(Customer c)
        {
            throw new NotImplementedException();
        }

        public void CustomerUpdate(Customer c)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerInParcelByPredicate(Predicate<Customer> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
