using System;
using DAL;
using DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    {
        enum Option { Add, Update,Display,Show };
        enum Add { Station, Dron, Customer, Parcel };
        enum Update {Join,Collect,Supply,SendDrone, ReleaseDrone, };
        enum Display { Station,Drone, Costumer,Parcel};
        enum Show{ Station, Drone, Costumer, Parcel ,ParcelWithoutDrone,AvilabaleStations};
        public static void addition(string[] args)
        {
            Console.WriteLine("insert: ADD for addtion\n Update for update\n Display for display\n Show for showing");
            Option x;

            string st = Console.ReadLine();
            x = (Option)int.Parse(st);
            switch (x)
            {
                case Option.Add:
                    Console.WriteLine("adding Station: Station\n adding Dron: Drone\n adding Customer: Customer\n adding Parcel: Parcel");
                    st = Console.ReadLine();
                    Add a = (Add)int.Parse(st);
                    switch (a)
                    {
                        case Add.Station:
                            Console.WriteLine("Insert id, name, langitude, latitude, charge slot");
                            break;
                        case Add.Dron:
                            Console.WriteLine("Insert id, model, maximum wight, drone status, battery");
                            break;
                        case Add.Customer:
                            Console.WriteLine("insert id, longitude, latitude");
                            customerAddition(System.Console.Read(), System.Console.Read(), System.Console.Read(), System.Console.Read());
                            break;
                        case Add.Parcel:
                            Console.WriteLine("insert id, weight, priority, requested, scheduled time, pickedUp time, delivered time");
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Update:
                    st = Console.ReadLine();
                    Update u = (Update)int.Parse(st);
                    switch (u)
                    {
                        case Update.Join:
                            break;
                        case Update.Collect:
                            break;
                        case Update.Supply:
                            break;
                        case Update.SendDrone:
                            break;
                        case Update.ReleaseDrone:
                            break;
                        default:
                            break;
                    }

                    break;
                case Option.Display:
                    st = Console.ReadLine();
                    Display d = (Display)int.Parse(st);
                    switch (d)
                    {
                        case Display.Station:
                            break;
                        case Display.Drone:
                            break;
                        case Display.Costumer:
                            break;
                        case Display.Parcel:
                            break;
                        default:
                            break;
                    }
                    break;
                case Option.Show:
                    st = Console.ReadLine();
                    Show s = (Show)int.Parse(st);
                    switch (s)
                    {
                        case Show.Station:
                            break;
                        case Show.Drone:
                            break;
                        case Show.Costumer:
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
                default:
                    break;
            }

            

        }


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
        }
      
}
}
