using BlApi;
using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Drone, Customer, Station, Join, PickedUpParcel, Supply, ChargeDrone, ReleaseDrone };//enum for update options
        enum Display { Station, Drone, Customer, Parcel };//enum for display options
        enum Show { Station, Drone, Customer, Parcel, ParcelWithoutDrone, AvilabaleStations };//enum for show options
        enum Weigth { Light, Medium, Heavy };
        enum Priorities { Regular, Fast, Emergency };

        static IBL ibl = BlFactory.GetBl();
        static void Main(string[] args)
        {
           
            Option o;
            Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for exit");
            string st = Console.ReadLine();
            bool b = Option.TryParse(st, out o);

            Location locat = new Location()
            {
                Longitude = 0,
                Latitude = 0
            };
            while (o != Option.Exit)//while not exit
            {
                try
                {
                    switch (o)
                    {

                        case Option.Add:
                            {
                                Console.WriteLine("insert: adding Station-0,\n adding Dron-1\n adding Customer-2\n adding Parce-3");
                                st = Console.ReadLine();
                                Add a;
                                b = Add.TryParse(st, out a);
                            
                                switch (a)
                                {
                                    case Add.Station:
                                        {
                                            Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber,Longitude, Latitude");
                                            Station stat = new Station()
                                            {
                                                Id = int.Parse(System.Console.ReadLine()),
                                                Name = System.Console.ReadLine(),
                                                AvailableChargeSlots = int.Parse(System.Console.ReadLine()),
                                              // DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
                                            };
                                            locat.Longitude = double.Parse(Console.ReadLine());
                                            locat.Latitude = double.Parse(Console.ReadLine());
                                            stat.Location = locat;
                                            ibl.AddStation(stat);
                                            break;
                                        }
                                    case Add.Dron:
                                        {
                                            Console.WriteLine("Insert id, model and Station ID");
                                            Drone dron = new Drone()
                                            {
                                                Id = int.Parse(System.Console.ReadLine()),
                                                Model = System.Console.ReadLine()
                                            };
                                            int statId = int.Parse(System.Console.ReadLine());
                                            Console.WriteLine("insert maximum weight: light-0, medium=1, heavy=2");
                                            int wght = int.Parse(System.Console.ReadLine());
                                            dron.Weight = (WeightCategories)wght;
                                            ibl.AddDrone(dron, statId);
                                            break;
                                        }
                                    case Add.Customer:
                                        {
                                            Console.WriteLine("Insert id, name, phone number, Longitude,Latitude ");
                                            Customer cust = new Customer()
                                            {
                                                Id = int.Parse(System.Console.ReadLine()),
                                                Name = System.Console.ReadLine(),
                                                Phone = System.Console.ReadLine(),
                                            };
                                            locat.Longitude = double.Parse(Console.ReadLine());
                                            locat.Latitude = double.Parse(Console.ReadLine());
                                            cust.Location = locat;
                                            ibl.AddCustomer(cust);
                                            break;
                                        }
                                    case Add.Parcel:
                                        {
                                            Console.WriteLine("Insert id of the sender and the target");
                                            int IdSend = int.Parse(System.Console.ReadLine());
                                            int IdReceive = int.Parse(System.Console.ReadLine());

                                            Console.WriteLine("insert Weight: light-0, medium=1, heavy=2");
                                            int wght = int.Parse(System.Console.ReadLine());
                                            Console.WriteLine("insert priority: Regular-0, Fast=1, Emergency=2");
                                            int prior = int.Parse(System.Console.ReadLine());

                                            Parcel parc = new Parcel()
                                            {
                                                Weight = (WeightCategories)wght,
                                                Priority = (BO.Priorities)prior
                                            };
                                            ibl.AddParcel(parc, IdSend, IdReceive);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                break;
                            }
                        case Option.Update:
                            {
                                Console.WriteLine("insert\n Updatong Drone-0,\n Updating Customer-1,\n Updating Station-2,\n Joining parcel to drone-3,\n Supply parcel by drone to customer-4,\n Charge Drone-5,\n Release Drone-6,\n Pick Up parcel by drone-7");
                                st = Console.ReadLine();
                                Update u;
                                b = Update.TryParse(st, out u);
                                switch (u)
                                {
                                    case Update.Drone:
                                        {
                                            Console.WriteLine("Insert id of drone and its new model");
                                            int idS = int.Parse(System.Console.ReadLine());
                                            string modelS = System.Console.ReadLine();
                                            ibl.UpdateDrone(idS, modelS);

                                            break;
                                        }
                                    case Update.Customer:
                                        {
                                            Console.WriteLine("Insert id of customer, its new name and phone(if not press 'enter')");
                                            int idC = int.Parse(System.Console.ReadLine());
                                            string nameC = System.Console.ReadLine();
                                            string phoneC = System.Console.ReadLine();
                                            ibl.UpdateCustomer(idC, nameC, phoneC);
                                            break;
                                        }
                                    case Update.Station:
                                        {
                                            Console.WriteLine("Insert id of station, its new name and num of ChargingPositions(if not press 'enter')");
                                            int idS = int.Parse(System.Console.ReadLine());
                                            string nameS = System.Console.ReadLine();
                                            string allChargingPositions = System.Console.ReadLine();
                                            ibl.UpdateStation(idS, nameS, allChargingPositions);
                                            break;
                                        }
                                    case Update.ChargeDrone:
                                        {
                                            Console.WriteLine("insert an id of a drone");
                                            int idD = int.Parse(System.Console.ReadLine());
                                            ibl.droneToCharge(idD);
                                            break;
                                        }
                                    case Update.ReleaseDrone:
                                        {
                                            Console.WriteLine("Insert id of drone and its time of charging in minutes");
                                            int idD = int.Parse(System.Console.ReadLine());
                                            TimeSpan t = TimeSpan.Parse(System.Console.ReadLine());
                                            ibl.ReleaseDroneFromCharge(idD, t);
                                            break;
                                        }
                                    case Update.Join:
                                        {
                                            Console.WriteLine("insert an id of a drone");
                                            int idD = int.Parse(System.Console.ReadLine());
                                            ibl.joinParcelToDrone(idD);
                                            break;
                                        }
                                    case Update.PickedUpParcel:
                                        {
                                            Console.WriteLine("Insert id of drone");
                                            int idD = int.Parse(System.Console.ReadLine());
                                            ibl.PickedUpParcelByDrone(idD);
                                            break;
                                        }
                                    case Update.Supply:
                                        {
                                            Console.WriteLine("Insert id of drone");
                                            int idD = int.Parse(System.Console.ReadLine());
                                            ibl.supplyParceByDrone(idD);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                break;
                            }
                        case Option.Display:
                            {
                                Console.WriteLine("Station-0, Drone-1, Customer-2, Parcel-3");
                                st = Console.ReadLine();
                                Display d;
                                b = Display.TryParse(st, out d);
                                int id;
                                switch (d)
                                {
                                    case Display.Station:
                                        {
                                            Console.WriteLine("insert station id number");
                                            st = Console.ReadLine();
                                            b = int.TryParse(st, out id);
                                            Station bs = ibl.GetStation(id);
                                            Console.WriteLine(bs);
                                            Console.WriteLine("The drones in charge are:");
                                            foreach(DroneCharging dc in bs.DroneCharging)
                                                Console.WriteLine(dc);
                                            break;
                                        }
                                    case Display.Drone:
                                        {
                                            Console.WriteLine("insert drone id number");
                                            st = Console.ReadLine();
                                            b = int.TryParse(st, out id);
                                            Drone dr = ibl.GetDrone(id);
                                            Console.WriteLine(dr);
                                            break;
                                        }
                                    case Display.Customer:
                                        {
                                            Console.WriteLine("insert customer id number");
                                            st = Console.ReadLine();
                                            b = int.TryParse(st, out id);
                                            Customer c = ibl.GetCustomer(id);
                                            Console.WriteLine(c);
                                            Console.WriteLine("The parcels From the customer:");
                                            foreach (ParcelAtCustomer pac in c.ParcelsFromCustomer)
                                                Console.WriteLine(pac);
                                            Console.WriteLine("\nThe parcels to the customer:");
                                            foreach (ParcelAtCustomer ptc in c.ParcelsToCustomer)
                                                Console.WriteLine(ptc);
                                            break;
                                        }
                                    case Display.Parcel:
                                        {
                                            Console.WriteLine("insert parcel id number");
                                            st = Console.ReadLine();
                                            b = int.TryParse(st, out id);
                                            Parcel p = ibl.GetParcel(id);
                                            Console.WriteLine(p);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                break;
                            }
                        case Option.Show:
                            {
                                Console.WriteLine("Station-0, Drone-1, Customer-2, Parcel-3 ,ParcelWithoutDrone-4, AvilabaleStations-5");
                                st = Console.ReadLine();
                                Show s;
                                b = Show.TryParse(st, out s);
                                switch (s)
                                {
                                    case Show.Station:
                                        {
                                            List<StationToList> lstStation = (List<StationToList>)ibl.GetAllStations();
                                            foreach (StationToList item in lstStation)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    case Show.Drone:
                                        {
                                            List<DroneToList> lstDrone = (List<DroneToList>)ibl.GetAllDrones(dr=>dr.Id==dr.Id);
                                            foreach (DroneToList item in lstDrone)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    case Show.Customer:
                                        {
                                            List<CustomerToList> lstCustomer = (List<CustomerToList>)ibl.GetAllCustomers();
                                            foreach (CustomerToList item in lstCustomer)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    case Show.Parcel:
                                        {
                                            List<ParcelToList> lstParcel = (List<ParcelToList>)ibl.GetAllParcels();
                                            foreach (ParcelToList item in lstParcel)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    case Show.ParcelWithoutDrone:
                                        {
                                            List<ParcelToList> lstParcel = (List<ParcelToList>)ibl.GetAllParcelsNotScheduled();
                                            foreach (ParcelToList item in lstParcel)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    case Show.AvilabaleStations:
                                        {
                                            List<StationToList> lstStation = (List<StationToList>)ibl.GetAllStationsWithAvailableSlots();
                                            foreach (StationToList item in lstStation)
                                                Console.WriteLine(item);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for exit");
                st = Console.ReadLine();
                b = Option.TryParse(st, out o);
            }
            
        }
    }
}