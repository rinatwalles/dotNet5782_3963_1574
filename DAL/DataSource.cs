using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal class config
        {
            internal static int droneCounter = 0;
            internal static int stationCounter = 0;
            internal static int customerCounter = 0;
            internal static int parcelsCounter = 0;
        }

        internal static Drone[] drones = new Drone[10];
        internal static Station[] stations = new Station[5];
        internal static Customer[] customers = new Customer[100];
        internal static Parcel[] parcels = new Parcel[1000];

        static Random rand = new Random(DateTime.Now.Millisecond);

        internal static void Initialize(int num)// צריך לראות איזה מספר זה num ,כי לכאורה צריך כמה נאמים לכל אתחול מספר אחר
        {
            for (int i = 0; i < 10; i++)
            {
                drones[i] = new Drone
                {
                    Id = rand.Next(10, 20),
                    //איתחול מודלים סמהאו
                    MaxWeight = (WeightCategories)rand.Next(0, 3),
                    Status = (DroneStatuses)rand.Next(0, 3),
                    Battery = rand.NextDouble()*100//לא יתן 1, רק קטן מ1!
                };
                config.droneCounter++;
            }


            for (int i = 0; i < 5; i++)
            {
                stations[i] = new Station
                {
                    Id = rand.Next(10, 20),
                    //איתחול מודלים סמהאו
                    //אתחול שם string
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                    ChargeSlots = rand.Next(0, 100)//יצטרכו לשנות 
                };
                config.stationCounter++;
            }

            for (int i = 0; i < 100; i++)
            {
                customers[i] = new Customer
                {
                    Id = rand.Next(100, 200),
                    //איתחול ניימס 
                    //איתחול טלפונים
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
                config.customerCounter++;
            }
            for (int i = 0; i < 1000; i++)
            {
                parcels[i] = new Parcel
                {
                    Id = rand.Next(1000, 2000),
                    SenderId = rand.Next(1000, 2000),
                    TargetId = rand.Next(1000, 2000),
                    Weight = (WeightCategories)rand.Next(1, 3),
                    Priority = (Priorities)rand.Next(1, 3),
                    Requested = DateTime.Now,//איתחול ריקוסטד
                    DroneId = rand.Next(10, 20),
                    Scheduled= DateTime.Now,//איתחול סקדולד
                    PickedUp= DateTime.Now,//איתחול פיקד אפ
                    Delivered= DateTime.Now,//איתחול דליברד
                };
            }

        }

    }
}

