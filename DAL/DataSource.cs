
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

        private static void createDrones(int num)
        {
            for (int i = 0; i < num; i++)
            {
                drones[i] = new Drone
                {
                    Id = rand.Next(10, 20),
                    //איתחול מודלים סמהאו
                    MaxWeight=(WeightCategories)rand.Next(0, 3),
                    Status=(DroneStatuses)rand.Next(0, 3),
                    Battery= rand.NextDouble()//לא יתן 1, רק קטן מ1!
            };
            }
        }
        private static void creatCustomer(int num)
        {
            for (int i = 0; i < num; i++)
            {
                customers[i] = new Customer
                {
                    Id = rand.Next(100, 200),
                    //איתחול ניימס 
                    //איתחול טלפונים
                    Longitude=rand.NextDouble()*(33.5-29.3)+29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
            }
        }
    }
}
