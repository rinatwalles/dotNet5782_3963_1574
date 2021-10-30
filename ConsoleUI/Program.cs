﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;
using static DalObject.DalObject;
namespace ConsoleUI
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Exit };
        enum Add { Station, Dron, Customer, Parcel };
        enum Update { Join, Collect, Supply, ChargeDrone, ReleaseDrone };
        enum Display { Station, Drone, Costumer, Parcel };
        enum Show { Station, Drone, Costumer, Parcel, ParcelWithoutDrone, AvilabaleStations };
        static void Main(string[] args)
        {
            Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for exit");
            string st = Console.ReadLine();
            Option x = (Option)int.Parse(st);
            while (x!=(Option)4)
            {
                switch (x)
                {
                    case Option.Add:
                        {
                            Console.WriteLine("insert: adding Station-0,\n adding Dron-1\n adding Customer-2\n adding Parce-3");
                            st = Console.ReadLine();
                            Add a = (Add)int.Parse(st);
                            switch (a)
                            {
                                case Add.Station:
                                    {
                                        Console.WriteLine("Insert id, name, langitude, latitude, charge slot");
                                        Station sstation = new Station()
                                        {
                                            Id = int.Parse(System.Console.ReadLine()),
                                            Name = System.Console.ReadLine(),
                                            Longitude = int.Parse(System.Console.ReadLine()),
                                            Latitude = int.Parse(System.Console.ReadLine()),
                                            ChargeSlots = int.Parse(System.Console.ReadLine())
                                        };
                                        stationAddition(sstation);
                                        break;
                                    }
                                case Add.Dron:
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
                                        droneAddition(ddrone);
                                        break;
                                    }
                                case Add.Customer:
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
                                        customerAddition(customer);
                                        break;
                                    }
                                case Add.Parcel:
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
                            Update u = (Update)int.Parse(st);
                            switch (u)
                            {
                                case Update.Join:
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        joinDroneToParcel(parcelId);
                                        break;
                                    }
                                case Update.Collect:
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        droneCollecting(parcelId);
                                        break;
                                    }
                                case Update.Supply:
                                    {
                                        Console.WriteLine("insert an id of a parcel");
                                        int parcelId = int.Parse(System.Console.ReadLine());
                                        customerCollecting(parcelId);
                                        break;
                                    }

                                case Update.ChargeDrone:
                                    {
                                        Console.WriteLine("insert an id of a drone and station");
                                        int droneId = int.Parse(System.Console.ReadLine());
                                        int stationId = int.Parse(System.Console.ReadLine());
                                        ChargingDrone(droneId, stationId);
                                        break;
                                    }
                                case Update.ReleaseDrone:
                                    {
                                        Console.WriteLine("insert an id of a drone and station");
                                        int droneId = int.Parse(System.Console.ReadLine());
                                        int stationId = int.Parse(System.Console.ReadLine());
                                        ReleaseDrone(droneId, stationId);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Display:
                        {
                            Console.WriteLine("Station-0, Drone-1, Costumer-2, Parcel-3");
                            st = Console.ReadLine();
                            Display d = (Display)int.Parse(st);
                            Console.WriteLine("insert id");
                            int id = int.Parse(System.Console.ReadLine());
                            switch (d)
                            {
                                case Display.Station:
                                    {
                                        printStation(id);
                                        break;
                                    }
                                case Display.Drone:
                                    {
                                        printDrone(id);
                                        break;
                                    }
                                case Display.Costumer:
                                    {
                                        printCustomer(id);
                                        break;
                                    }
                                case Display.Parcel:
                                    {
                                        printParcel(id);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case Option.Show:
                        {
                            Console.WriteLine("Station-0, Drone-1, Costumer-2, Parcel-3 ,ParcelWithoutDrone-4, AvilabaleStations-5\n");
                            st = Console.ReadLine();
                            Show s = (Show)int.Parse(st);
                            switch (s)
                            {
                                case Show.Station:
                                    { printAllStation(); break; }
                                case Show.Drone:
                                    { printAllDrones(); break; }
                                case Show.Costumer:
                                    { printAllCustomer(); break; }
                                case Show.Parcel:
                                    { printAllParcels(); break; }
                                case Show.ParcelWithoutDrone:
                                    { ParcelWithoutDrone(); break; }
                                case Show.AvilabaleStations:
                                    { printEmptyCargeSlots(); break; }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
             Console.WriteLine("insert:\n 0 for addtion\n 1 for update\n 2 for display\n 3 for showing\n 4 for exit");
             st = Console.ReadLine();
             x = (Option)int.Parse(st);
            }
        }
    }
}

