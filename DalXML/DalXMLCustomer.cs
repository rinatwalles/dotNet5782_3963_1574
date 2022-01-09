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
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            return customersRootElem.Elements().Any(c => Int32.Parse(c.Element("Id").Value) == id);
        }
        

        public void CustomerAddition(Customer c)
        {
            if (CheckCustomer(c.Id))
                throw new DuplicateIdException(c.Id, "Customer");
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            XElement personElem = new XElement("Person", new XElement("Id", c.Id),
                                  new XElement("Name", c.Name),
                                  new XElement("Phone", c.Phone),
                                  new XElement("Longitude", c.Longitude),
                                  new XElement("Latitude", c.Latitude));

            customersRootElem.Add(personElem);

            XMLTools.SaveListToXMLElement(customersRootElem, CustomersPath);
        }

        public Customer GetCustomer(int id)
        {
            if (!CheckCustomer(id))
                throw new MissingIdException(id, "Customer");
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            Customer cust = (from c in customersRootElem.Elements()
                             where int.Parse(c.Element("Id").Value) == id
                             select new Customer()
                             {
                                 Id = Int32.Parse(c.Element("Id").Value),
                                 Name = c.Element("Name").Value,
                                 Phone = c.Element("Phone").Value,
                                 Longitude=double.Parse(c.Element("Longitude").Value),
                                 Latitude = double.Parse(c.Element("Latitude").Value)
                             }
                             ).FirstOrDefault();
           
            return cust;
        }

       
        public IEnumerable<Customer> AllCustomer()
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            return (from c in customersRootElem.Elements()
                    select new Customer()
                    {
                        Id = Int32.Parse(c.Element("Id").Value),
                        Name = c.Element("Name").Value,
                        Phone = c.Element("Phone").Value,
                        Longitude = double.Parse(c.Element("Longitude").Value),
                        Latitude = double.Parse(c.Element("Latitude").Value)
                    }
                    );
        }

       
        //public void DeletePerson(int id)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    XElement per = (from p in personsRootElem.Elements()
        //                    where int.Parse(p.Element("ID").Value) == id
        //                    select p).FirstOrDefault();

        //    if (per != null)
        //    {
        //        per.Remove(); //<==>   Remove per from personsRootElem

        //        XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        //}
        public void CustomerDelete(Customer c)//??
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            XElement per = (from p in customersRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == c.Id
                            select p).FirstOrDefault();
        }
        public void CustomerUpdate(Customer c)
        {
            if (!CheckCustomer(c.Id))
                throw new MissingIdException(c.Id, "Customer");
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            XElement cust = (from newC in customersRootElem.Elements()
                            where int.Parse(newC.Element("Id").Value) == c.Id
                            select newC).FirstOrDefault();
            cust.Element("Id").Value = c.Id.ToString();
            cust.Element("Name").Value = c.Name;
            cust.Element("Phone").Value = c.Phone;
            cust.Element("Longitude").Value = c.Longitude.ToString();
            cust.Element("Latitude").Value = c.Latitude.ToString();

            XMLTools.SaveListToXMLElement(customersRootElem, CustomersPath);
        }

        public IEnumerable<Customer> GetCustomerByPredicate(Predicate<Customer> predicate)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(CustomersPath);
            return from cust in customersRootElem.Elements()
                   let c = new Customer()
                   {
                       Id = Int32.Parse(cust.Element("Id").Value),
                       Name = cust.Element("Name").Value,
                       Phone = cust.Element("Phone").Value,
                       Longitude = double.Parse(cust.Element("Longitude").Value),
                       Latitude = double.Parse(cust.Element("Latitude").Value)
                   }
                   where predicate(c)
                   select c;
        }
    }
}
