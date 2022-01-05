using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal
{
    internal class DataSource
    {
        /// <summary>
        /// function that create static arrays for drones, stations, customers, parcels and drone charges
        /// </summary>
        internal class Config
        {
            static public int ParcelId = 1;
            static public double Availavble = 0.001;
            static public double Light = 0.003;
            static public double Medium = 0.004;
            static public double Heavy = 0.005;
            static public double ChargePrecent = 0.35;  //קצב טעינת רחפן
        }

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
            for (int i = 1; i <= 15; i++)
            {
                Drone d = new Drone()
                {
                    Id = i,
                    Model = "" + (char)rand.Next(65, 90) + (char)rand.Next(97, 122) + (char)rand.Next(97, 122),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    // Status = (DroneStatuses)rand.Next(0, 1),
                    // Battery = rand.NextDouble()*100//לא יתן 1, רק קטן מ1!
                };
                drones.Add(d);
            }

            for (int i = 1; i <= 15; i++)
            {
                Station s = new Station()
                {
                    Id = i,
                    Name = "" + (char)rand.Next(65, 90) + (char)rand.Next(97, 122) + (char)rand.Next(97, 122),
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                    AvailableChargeSlots = rand.Next(2, 10)   //number of empty charge slots in each station
                };
                stations.Add(s);
            }

            for (int i = 1; i <= 15; i++)
            {
                Customer c = new Customer()
                {
                    Id = i,
                    Name = "" + (char)rand.Next(65, 90) + (char)rand.Next(97, 122) + (char)rand.Next(97, 122),
                    Phone = "054" + (char)rand.Next(48, 57) + (char)rand.Next(48, 57) + (char)rand.Next(48, 57) + (char)rand.Next(48, 57) + (char)rand.Next(48, 57) + (char)rand.Next(48, 57),
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
                customers.Add(c);
            }

            for (int i = 1; i <= 3; i++)
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 10),
                    TargetId = rand.Next(1, 10),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(1, 20),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.MinValue,
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                parcels.Add(p);
            }
            for (int i = 4; i <= 7; i++)
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 10),
                    TargetId = rand.Next(1, 10),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(1, 20),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Today.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                parcels.Add(p);
            }

            for (int i = 8; i <= 10; i++)
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 10),
                    TargetId = rand.Next(1, 10),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(1, 20),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Today.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.Today.AddHours(rand.Next(10, 20)),
                    DeliveredTime = DateTime.MinValue
                };
                parcels.Add(p);
            }
            for (int i = 11; i <= 15; i++)
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 10),
                    TargetId = rand.Next(1, 10),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(1, 20),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Today.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.Today.AddHours(rand.Next(10, 15)),
                    DeliveredTime = DateTime.Today.AddHours(rand.Next(15, 16)),
                };
                parcels.Add(p);
            }


        }

    }
}


