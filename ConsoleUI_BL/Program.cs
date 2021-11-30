using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;
using IBL.BO;

namespace ConsoleUI_BL
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Distance, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Drone, Customer, Station, Join, Collect, Supply, ChargeDrone, ReleaseDrone, PickedUpParcel };//enum for update options
        enum Display { Station, Drone, Customer, Parcel };//enum for display options
        enum Show { Station, Drone, Customer, Parcel, ParcelWithoutDrone, AvilabaleStations };//enum for show options
        enum Weigth { Light, Medium, Heavy };
        enum Priorities { Regular, Fast, Emergency };
        static void Main(string[] args)
        {
            IBL.IBL ibl = new BL.BL();
            Option o;
            Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for distance\n 5 for exit");
            string st = Console.ReadLine();
            bool b = Option.TryParse(st, out o);
            // קודם כל מיקום
            IBL.BO.Location locat = new IBL.BO.Location()
            {
                Longitude = 0, Latitude = 0
            };
            while (o != Option.Exit)//while not exit
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
                                        Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber");
                                        IBL.BO.Station stat = new IBL.BO.Station()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Name = System.Console.ReadLine(),
                                            AvailableChargeSlots = int.Parse(System.Console.ReadLine()),
                                            DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
                                        };
                                        locat.Longitude = double.Parse(Console.ReadLine());
                                        locat.Latitude = double.Parse(Console.ReadLine());
                                        stat.Location = locat;
                                        ibl.AddStation(stat);
                                        break;
                                    }
                                case Add.Dron:
                                    {//קליטת רחפן
                                        Console.WriteLine("Insert id, model, maximum wight");
                                        IBL.BO.Drone dron = new IBL.BO.Drone()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Model = System.Console.ReadLine(),
                                            Weight = (IBL.BO.WeightCategories)System.Console.Read(),
                                        };
                                        int statId = int.Parse(System.Console.ReadLine());
                                        ibl.AddDrone(dron, statId);
                                        break;
                                    }
                                case Add.Customer:
                                    {//קליטת לקוח
                                        Console.WriteLine("Insert id, name, number, location");
                                        IBL.BO.Customer cust = new IBL.BO.Customer()
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
                                    {//קליטת חבילה
                                        Console.WriteLine("Insert idof the sender and the target");
                                        int IdSend = int.Parse(System.Console.ReadLine());
                                        int IdReceive = int.Parse(System.Console.ReadLine());

                                        Console.WriteLine("insert Weight: light-0, medium=1, heavy=2");
                                        int wght = int.Parse(System.Console.ReadLine());
                                        Console.WriteLine("insert priority: Regular-0, Fast=1, Emergency=2");
                                        int prior = int.Parse(System.Console.ReadLine());

                                        IBL.BO.Parcel parc = new IBL.BO.Parcel()
                                        {
                                            Weight= (IBL.BO.WeightCategories)wght,
                                            Priority= (IBL.BO.Priorities)prior
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
                            Console.WriteLine("insert: Join-0, Collect=1, Supply=2, ChargeDrone=3, ReleaseDrone=4");
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
                                        Console.WriteLine("Insert id of drone and its time of charging");
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
                            Console.WriteLine("insert station id number");
                            st = Console.ReadLine();
                            b = Display.TryParse(st, out id);
                            switch (d)
                            {
                                case Display.Station:
                                    {
                                        IBL.BO.Station bs = ibl.getBaseStation(id);
                                        Console.WriteLine(bs);
                                        break;
                                    }
                                case Display.Drone:
                                    {
                                        IBL.BO.Drone dr = ibl.getDrone(id);
                                        Console.WriteLine(dr);
                                        break;
                                    }
                                case Display.Customer:
                                    {
                                        IBL.BO.Customer c = ibl.GetCustomer(id);
                                        Console.WriteLine(c);
                                        break;
                                    }
                                case Display.Parcel:
                                    {
                                        IBL.BO.Parcel p = ibl.getParcel(id);
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
                                        List<DroneToList> lstDrone = (List<DroneToList>)ibl.GetAllDrones();
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
                Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for distance\n 5 for exit");
                st = Console.ReadLine();
                b = Option.TryParse(st, out o);
            }
            
        }
    }
}     

