using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;

namespace ConsoleUI
{
    public class ProgramA
    {
        enum Option { Add, Update, Display, Show, Distance, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Join, Collect, Supply, ChargeDrone, ReleaseDrone };//enum for update options
        enum Display { Station, Drone, Customer, Parcel };//enum for display options
        enum Show { Station, Drone, Customer, Parcel, ParcelWithoutDrone, AvilabaleStations };//enum for show options
        enum Distance { Station, Customer }//enum for distance options
        static void Main(string[] args)
        {
            DalObject.DalObject dall = new DalObject.DalObject();
            Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for distance\n 5 for exit");
            string st = Console.ReadLine();
            Option x;
            bool b = Option.TryParse(st, out x);
            while (x!=Option.Exit)//while not exit
            {
                switch (x)
                {
                    case Option.Add://addint new object
                        {
                            Console.WriteLine("insert: adding Station-0,\n adding Dron-1\n adding Customer-2\n adding Parce-3");
                            st = Console.ReadLine();
                            Add a;
                            b = Add.TryParse(st, out a);
                            switch (a)
                            {
                                case Add.Station://add station
                                    {
                                        Console.WriteLine("Insert id, name, langitude, latitude, charge slot");
                                        IDAL.DO.Station sstation = new IDAL.DO.Station()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Name = System.Console.ReadLine(),
                                            Longitude = int.Parse(System.Console.ReadLine()),
                                            Latitude = int.Parse(System.Console.ReadLine()),
                                            ChargeSlots = int.Parse(System.Console.ReadLine())
                                        };
                                        dall.StationAddition(sstation);
                                        break;
                                    }
                                case Add.Dron://add drone
                                    {
                                        Console.WriteLine("Insert id, model, maximum wight");
                                        Drone ddrone = new Drone()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Model = System.Console.ReadLine(),
                                            MaxWeight = (WeightCategories)System.Console.Read(),
                                            Status = DroneStatuses.Available,
                                            Battery = 100,
                                        };
                                        dall.DroneAddition(ddrone);
                                        break;
                                    }
                                case Add.Customer://add drone
                                    {
                                        Console.WriteLine("insert id, longitude, latitude");
                                        Customer customer = new Customer()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Name = System.Console.ReadLine(),
                                            Phone = System.Console.ReadLine(),
                                            Longitude = int.Parse(System.Console.ReadLine()),
                                            Latitude = int.Parse(System.Console.ReadLine())
                                        };
                                        dall.CustomerAddition(customer);
                                        break;
                                    }
                                case Add.Parcel://add parcel
                                    {
                                        Console.WriteLine("insert id, SenderId, TargetId, weight, Priority");
                                        Parcel parcel = new Parcel()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            SenderId = int.Parse(System.Console.ReadLine()),
                                            TargetId = int.Parse(System.Console.ReadLine()),
                                            Weight = (WeightCategories)System.Console.Read(),
                                            Priority = (Priorities)System.Console.Read(),
                                            Requested = DateTime.Now,
                                            DroneId = 0
                                        };
                                        dall.ParcelAddition(parcel);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Update://upadte the objects
                        {
                            Console.WriteLine("insert: Join-0, Collect=1, Supply=2, ChargeDrone=3, ReleaseDrone=4");
                            st = Console.ReadLine();
                            Update u;
                            b = Update.TryParse(st, out u);
                            switch (u)
                            {
                                case Update.Join:// join drone to parcel
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        dall.JoinDroneToParcel(parcelId);
                                        break;
                                    }
                                case Update.Collect://parcel collecting by drine 
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        dall.DroneCollecting(parcelId);
                                        break;
                                    }
                                case Update.Supply://customer collecting the parcel
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        dall.CustomerCollecting(parcelId);
                                        break;
                                    }

                                case Update.ChargeDrone://chraging the drone
                                    {
                                        Console.WriteLine("insert an id of a drone and station");
                                        int droneId = int.Parse(System.Console.ReadLine());
                                        int stationId = int.Parse(System.Console.ReadLine());
                                        dall.ChargingDrone(droneId, stationId);
                                        break;
                                    }
                                case Update.ReleaseDrone://releasing the drone
                                    {
                                        Console.WriteLine("insert an id of a drone and station");
                                        int droneId = int.Parse(System.Console.ReadLine());
                                        int stationId = int.Parse(System.Console.ReadLine());
                                        dall.ReleaseDrone(droneId, stationId);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Display://display
                        {
                            Console.WriteLine("Station-0, Drone-1, Customer-2, Parcel-3");
                            st = Console.ReadLine();
                            Display d;
                            b = Display.TryParse(st, out d);
                            Console.WriteLine("insert id");
                            int id = int.Parse(System.Console.ReadLine());
                            switch (d)
                            {
                                case Display.Station://print the station
                                    {
                                        Station s=dall.PrintStation(id);
                                        Console.WriteLine(s);
                                        break;
                                    }
                                case Display.Drone://print the drone
                                    {
                                        Drone dr=dall.PrintDrone(id);
                                        Console.WriteLine(dr);
                                        break;
                                    }
                                case Display.Customer://print the Customer
                                    {
                                        Customer c=dall.PrintCustomer(id);
                                        Console.WriteLine(c);
                                        break;
                                    }
                                case Display.Parcel://print the parcel
                                    {
                                        Parcel p=dall.PrintParcel(id);
                                        Console.WriteLine(p);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Show://show
                        {
                            Console.WriteLine("Station-0, Drone-1, Customer-2, Parcel-3 ,ParcelWithoutDrone-4, AvilabaleStations-5");
                            st = Console.ReadLine();
                            Show s ;
                            b = Show.TryParse(st, out s);
                            switch (s)
                            {
                                case Show.Station://print all stations
                                    { dall.PrintAllStation(); break; }
                                case Show.Drone://print all drones
                                    { dall.PrintAllDrones(); break; }
                                case Show.Customer://print all Customer
                                    { dall.PrintAllCustomer(); break; }
                                case Show.Parcel://print all parcels
                                    { dall.PrintAllParcels(); break; }
                                case Show.ParcelWithoutDrone://orint all parcels without drones
                                    { dall.ParcelWithoutDrone(); break; }
                                case Show.AvilabaleStations://print all avilable stations
                                    { dall.PrintEmptyCargeSlots(); break; }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Distance:
                        {
                            Console.WriteLine("insert a cordinate");
                            st = Console.ReadLine();
                            double x1; 
                            b = double.TryParse(st,out x1);
                            st = Console.ReadLine();
                            double y1;
                            b = double.TryParse(st,out y1);
                            Console.WriteLine("choose distance from: Station-0, Costumer-1,");
                            st = Console.ReadLine();
                            Distance ds;
                            b= Distance.TryParse(st,out ds);
                            switch (ds)
                            {
                                case Distance.Station:
                                    {
                                        Console.WriteLine("insert an id of a station");
                                        st = Console.ReadLine();
                                        int id = int.Parse(st);
                                        Console.WriteLine(dall.DistanceFromStation(id, x1, y1));
                                        break;
                                    }
                                case Distance.Costumer:
                                    {
                                        Console.WriteLine("insert an id of a customer");
                                        st = Console.ReadLine();
                                        int id = int.Parse(st);
                                        Console.WriteLine(dall.DistanceFromCustomer(id, x1, y1));
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
             Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for distance\n 5 for exit");
             st = Console.ReadLine();
             b = Option.TryParse(st, out x);
            }
        }
    }
}

