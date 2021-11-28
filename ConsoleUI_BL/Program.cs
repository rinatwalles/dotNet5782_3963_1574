using IBL.BO;
using System;

namespace ConsoleUI_BL
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Distance, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Join, Collect, Supply, ChargeDrone, ReleaseDrone };//enum for update options
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
                            //קליטת תחנת בסיס, קודם כל מיקום
                            IBL.BO.Location locate = new IBL.BO.Location()
                            {
                                Longitude = 0,
                                Latitude = 0
                            };

                            Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber");
                            IBL.BO.BaseStation sstation = new IBL.BO.BaseStation()
                            {
                                Id = int.Parse(System.Console.ReadLine()),
                                Name = System.Console.ReadLine(),
                                AvilableChargeSlotsNumber = int.Parse(System.Console.ReadLine()),
                                DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
                            };
                            locate.Longitude = double.Parse(Console.ReadLine());
                            locate.Latitude = double.Parse(Console.ReadLine());
                            sstation.SLocation = locate;
                            ibl.AddBaseStation(sstation);
                            break;
                        case Add.Dron:
                            //קליטת רחפן
                            Console.WriteLine("Insert id, model, maximum wight");
                            IBL.BO.Drone ddrone = new IBL.BO.Drone()
                            {
                                Id = int.Parse(System.Console.ReadLine()),
                                Model = System.Console.ReadLine(),
                                Weight = (WeightCategories)System.Console.Read(),
                            };
                            int sId = int.Parse(System.Console.ReadLine());
                            ibl.AddDrone(ddrone, sId);
                            break;
                        case Add.Customer:
                            //קליטת לקוח
                            Console.WriteLine("Insert id, name, number, location");
                            IBL.BO.Customer ccustomer = new IBL.BO.Customer()
                            {
                                Id = int.Parse(System.Console.ReadLine()),
                                Name = System.Console.ReadLine(),
                                Phone = System.Console.ReadLine(),
                            };
                            locate.Longitude = double.Parse(Console.ReadLine());
                            locate.Latitude = double.Parse(Console.ReadLine());
                            ccustomer.Place = locate;
                            ibl.AddCustomer(ccustomer);
                            break;
                        case Add.Parcel:
                            //קליטת חבילה
                            Console.WriteLine("Insert id, name, number, location");
                            IBL.BO.Parcel pparcel = new IBL.BO.Parcel()
                            {
                                //לעשות תת תפריט למשקל ועדיפות
                            };
                            int IdSender = int.Parse(System.Console.ReadLine());
                            int IdReceiver = int.Parse(System.Console.ReadLine());
                            ibl.AddParcel(pparcel, IdSender, IdReceiver);
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
                        case Update.Join:
                            break;
                        case Update.Collect:
                            break;
                        case Update.Supply:
                            break;
                        case Update.ChargeDrone:
                            break;
                        case Update.ReleaseDrone:
                            break;
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
                            BaseStation bs = ibl.getBaseStation(id);
                            Console.WriteLine(bs);
                            break;
                        case Display.Drone:
                            Drone dr = ibl.getDrone(id);
                            Console.WriteLine(dr);
                            break;
                        case Display.Customer:
                            Customer c = ibl.GetCustomer(id);
                            Console.WriteLine(c);
                            break;
                        case Display.Parcel:
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
        }
    }
}     

