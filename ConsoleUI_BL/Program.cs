using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;

namespace ConsoleUI_BL
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Distance, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Drone, Customer, Station, Join, Collect, Supply, ChargeDrone, ReleaseDrone };//enum for update options
        enum Display { Station, Drone, Customer, Parcel };//enum for display options
        enum Show { Station, Drone, Customer, Parcel, ParcelWithoutDrone, AvilabaleStations };//enum for show options
        enum Distance { Station, Customer }//enum for distance options
        static void Main(string[] args)
        {
            IBL.IBL ibl = new BL.BL();
            Option o;
            Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for distance\n 5 for exit");
            string st = Console.ReadLine();
            bool b = Option.TryParse(st, out o);
            //קליטת תחנת בסיס, קודם כל מיקום
            IBL.BO.Location locat = new IBL.BO.Location()
            {
                Longitude = 0, Latitude = 0
            };
            switch (o)
            {

                case Option.Add:
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
                                ibl.AddBaseStation(stat);
                                break;
                            }
                        case Add.Dron:
                            //קליטת רחפן
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
                        case Add.Customer:
                            //קליטת לקוח
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
                        case Add.Parcel:
                            //קליטת חבילה
                            Console.WriteLine("Insert id, name, number, location");
                            IBL.BO.Parcel parc = new IBL.BO.Parcel()
                            {
                                //לעשות תת תפריט למשקל ועדיפות
                            };
                            int IdSend = int.Parse(System.Console.ReadLine());
                            int IdReceive = int.Parse(System.Console.ReadLine());
                            ibl.AddParcel(parc, IdSend, IdReceive);
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Update:
                    Console.WriteLine("insert: Join-0, Collect=1, Supply=2, ChargeDrone=3, ReleaseDrone=4");
                    st = Console.ReadLine();
                    Update u;
                    b = Update.TryParse(st, out u);
                    switch (u)
                    {
                        case Update.Drone:
                            {
                                Console.WriteLine("Insert id of drone and its new name");
                                int idS = int.Parse(System.Console.ReadLine());
                                string modelS = System.Console.ReadLine();
                                ibl.UpdateDrone(idS, modelS);

                                break;
                            }
                        case Update.Customer:
                            {
                                Console.WriteLine("Insert id of customer and its new name");
                                int idC = int.Parse(System.Console.ReadLine());
                                string nameC = System.Console.ReadLine();
                                string phoneC = System.Console.ReadLine();
                                ibl.UpdateCustomer(idC, nameC, phoneC);
                                break;
                            }
                        case Update.Station:
                            {
                                Console.WriteLine("Insert id of station and its new name");
                                int idS = int.Parse(System.Console.ReadLine());
                                string nameS = System.Console.ReadLine();
                                string allChargingPositions = System.Console.ReadLine();
                                ibl.UpdateCustomer(idS, nameS, allChargingPositions);
                                Console.WriteLine("insert an id of a drone");
                                int idD= int.Parse(System.Console.ReadLine());
                                ibl.droneToCharge(idD);
                                break;
                            }
                        case Update.ChargeDrone:
                            break;
                        case Update.ReleaseDrone:
                            {
                                Console.WriteLine("Insert id of drone and its time of charging");
                                int idD = int.Parse(System.Console.ReadLine());
                                TimeSpan t = TimeSpan.Parse(System.Console.ReadLine());
                                ibl.ReleaseDroneFromCharge(idD, t);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                case Option.Display:
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
                            IBL.BO.Station bs = ibl.getBaseStation(id);
                            Console.WriteLine(bs);
                            break;
                        case Display.Drone:
                            IBL.BO.Drone dr = ibl.getDrone(id);
                            Console.WriteLine(dr);
                            break;
                        case Display.Customer:
                            IBL.BO.Customer c = ibl.GetCustomer(id);
                            Console.WriteLine(c);
                            break;
                        case Display.Parcel:
                            IBL.BO.Parcel p = ibl.getParcel(id);
                            Console.WriteLine(p);
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Show:
                    Console.WriteLine("Station-0, Drone-1, Customer-2, Parcel-3 ,ParcelWithoutDrone-4, AvilabaleStations-5");
                    st = Console.ReadLine();
                    Show s;
                    b = Show.TryParse(st, out s);
                    switch (s)
                    {
                        case Show.Station:
                            break;
                        case Show.Drone:
                            break;
                        case Show.Customer:
                            break;
                        case Show.Parcel:
                            break;
                        case Show.ParcelWithoutDrone:
                            break;
                        case Show.AvilabaleStations:
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Distance:
                    Console.WriteLine("insert a cordinate");
                    st = Console.ReadLine();
                    double x1;
                    b = double.TryParse(st, out x1);
                    st = Console.ReadLine();
                    double y1;
                    b = double.TryParse(st, out y1);
                    Console.WriteLine("choose distance from: Station-0, Costumer-1,");
                    st = Console.ReadLine();
                    Distance ds;
                    b = Distance.TryParse(st, out ds);
                    switch (ds)
                    {
                        case Distance.Station:
                            break;
                        case Distance.Customer:
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Exit:
                    break;
                default:
                    break;
            }
            //IBL.IBL ibl = new BL.BL();
            ////קליטת תחנת בסיס, קודם כל מיקום
            //IBL.BO.Location locate = new IBL.BO.Location()
            //{
            //    Longitude = 0,
            //    Latitude = 0
            //};

            //Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber");
            //IBL.BO.BaseStation sstation = new IBL.BO.BaseStation()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Name = System.Console.ReadLine(),
            //    AvilableChargeSlotsNumber = int.Parse(System.Console.ReadLine()),
            //    DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
            //};
            //locate.Longitude = double.Parse(Console.ReadLine());
            //locate.Latitude = double.Parse(Console.ReadLine());
            //sstation.SLocation = locate;
            //ibl.AddBaseStation(sstation);
            ////קליטת רחפן
            //Console.WriteLine("Insert id, model, maximum wight");
            //IBL.BO.Drone ddrone = new IBL.BO.Drone()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Model = System.Console.ReadLine(),
            //    Weight = (WeightCategories)System.Console.Read(),
            //};
            //int sId = int.Parse(System.Console.ReadLine());
            //ibl.AddDrone(ddrone, sId);
            //קליטת לקוח
            //Console.WriteLine("Insert id, name, number, location");
            //IBL.BO.Customer ccustomer = new IBL.BO.Customer()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Name = System.Console.ReadLine(),
            //    Phone = System.Console.ReadLine(),
            //};
            //locate.Longitude = double.Parse(Console.ReadLine());
            //locate.Latitude = double.Parse(Console.ReadLine());
            //ccustomer.Place = locate;
            //ibl.AddCustomer(ccustomer);
            //קליטת חבילה
            //Console.WriteLine("Insert id, name, number, location");
            //IBL.BO.Parcel pparcel = new IBL.BO.Parcel()
            //{
            //    //לעשות תת תפריט למשקל ועדיפות
            //};
            //int IdSender = int.Parse(System.Console.ReadLine());
            //int IdReceiver = int.Parse(System.Console.ReadLine());
            //ibl.AddParcel(pparcel, IdSender, IdReceiver);
            // קודם כל מיקום בשביל כמה אוביקטים

            ////קליטת תחנת בסיס,
            //Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber");
            //IBL.BO.Station sstation = new IBL.BO.Station()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Name = System.Console.ReadLine(),
            //    AvailableChargeSlots = int.Parse(System.Console.ReadLine()),
            //    DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
            //};
            //locat.Longitude = double.Parse(Console.ReadLine());
            //locat.Latitude = double.Parse(Console.ReadLine());
            //sstation.Location = locat;
            //ibl.AddBaseStation(sstation);
            ////קליטת רחפן
            //Console.WriteLine("Insert id, model, maximum wight");
            //IBL.BO.Drone ddrone = new IBL.BO.Drone()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Model = System.Console.ReadLine(),
            //    Weight = (IBL.BO.WeightCategories)System.Console.Read(),
            //};
            //int sId = int.Parse(System.Console.ReadLine());
            //ibl.AddDrone(ddrone, sId);
            ////קליטת לקוח
            //Console.WriteLine("Insert id, name, number, location");
            //IBL.BO.Customer ccustomer = new IBL.BO.Customer()
            //{
            //    Id = int.Parse(System.Console.ReadLine()),
            //    Name = System.Console.ReadLine(),
            //    Phone = System.Console.ReadLine(),
            //};
            //locate.Longitude = double.Parse(Console.ReadLine());
            //locate.Latitude = double.Parse(Console.ReadLine());
            //ccustomer.Place = locate;
            //ibl.AddCustomer(ccustomer);
            ////קליטת חבילה
            //Console.WriteLine("Insert id, name, number, location");
            //IBL.BO.Parcel pparcel = new IBL.BO.Parcel()
            //{
            //    //לעשות תת תפריט למשקל ועדיפות
            //};
            //int IdSender = int.Parse(System.Console.ReadLine());
            //int IdReceiver = int.Parse(System.Console.ReadLine());
            //ibl.AddParcel(pparcel, IdSender, IdReceiver);


        }
    }
}     

