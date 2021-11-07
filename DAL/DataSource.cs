﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        /// <summary>
        /// function that create static arrays for drones, stations, customers, parcels and drone charges
        /// </summary>
        //internal class config
        //{
        //    internal static int droneCounter = 0;
        //    internal static int stationCounter = 0;
        //    internal static int customerCounter = 0;
        //    internal static int parcelsCounter = 0;
        //    internal static int droneChargeCounter = 0;
        //}

        //internal static Drone[] drones = new Drone[10];
        //internal static Station[] stations = new Station[5];
        //internal static Customer[] customers = new Customer[100];
        //internal static Parcel[] parcels = new Parcel[1000];
        //internal static DroneCharge[] droneCharges = new DroneCharge[10];

        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        static Random rand = new Random(DateTime.Now.Millisecond);
        
        /// <summary>
        /// The function initialize the arrays
        /// </summary>
        internal static void Initialize()// צריך לראות איזה מספר זה num ,כי לכאורה צריך כמה נאמים לכל אתחול מספר אחר
        {
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                //config.droneCounter++;
                drones[i] = new Drone ()
                {
                    Id =num++,
                    Model = "abc",
                    MaxWeight = (WeightCategories)rand.Next(0, 2),
                   // Status = (DroneStatuses)rand.Next(0, 1),
                   // Battery = rand.NextDouble()*100//לא יתן 1, רק קטן מ1!
                };
            }

            for (int i = 0; i < 2; i++)
            {
                //config.stationCounter++;
                stations[i] = new Station()
                {
                    Id = num++,
                    Name = "ghi",
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                    ChargeSlots = rand.Next(0, 10)   //number of empty charge slots in each station
                };
            }

            for (int i = 0; i < 10; i++)
            {
                //config.customerCounter++;
                customers[i] = new Customer()
                {
                    Id = num++,
                    Name ="def",
                    Phone= "12345678",
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
            }

            for (int i = 0; i < 10; i++)
            {
                //config.parcelsCounter++;
                parcels[i] = new Parcel()
                {
                    Id = num++,
                    SenderId = rand.Next(1000, 2000),
                    TargetId = rand.Next(1000, 2000),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    Requested = DateTime.Now,
                    DroneId = rand.Next(10, 20),
                    Scheduled= parcels[i].Requested.AddHours(1),
                    PickedUp= parcels[i].Requested.AddHours(1),
                    Delivered= parcels[i].Requested.AddHours(1),
                };
            }

        }

    }
}

