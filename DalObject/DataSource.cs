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
            static public double Availavble = 0.01;
            static public double Light = 0.03;
            static public double Medium = 0.04;
            static public double Heavy = 0.05;
            static public double ChargePrecent = 0.35;  //קצב טעינת רחפן
        }

        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        //public static string CustomersPath = "Customers.xml";
        //public static string ParcelsPath = "Parcels.xml";
        //public static string DronesPath = "Drones.xml";
        //public static string StationsPath = "Stations.xml";
        //public static string DroneChargesPath = "DroneCharges.xml";


        static Random rand = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The function initialize the arrays
        /// </summary>
        internal static void Initialize()// צריך לראות איזה מספר זה num ,כי לכאורה צריך כמה נאמים לכל אתחול מספר אחר
        {
            for (int i = 1; i <= 5; i++)
            {
                Drone d = new Drone()
                {
                    Id = i,
                    Model = "" + (char)rand.Next(65, 90) + (char)rand.Next(97, 122) + (char)rand.Next(97, 122),
                    Weight = (WeightCategories)rand.Next(0, 3),
                    // Status = (DroneStatuses)rand.Next(0, 1),
                    // Battery = rand.NextDouble()*100//לא יתן 1, רק קטן מ1!
                };
                drones.Add(d);
            }

            for (int i = 1; i <= 5; i++)
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

            for (int i = 1; i <= 5; i++)
            {
                Customer c = new Customer()
                {
                    Id = i,
                    Name = "" + (char)rand.Next(65, 90) + (char)rand.Next(97, 122) + (char)rand.Next(97, 122),
                    Phone = "054" + (char)rand.Next(48, 58) + (char)rand.Next(48, 58) + (char)rand.Next(48, 58) + (char)rand.Next(48, 58) + (char)rand.Next(48, 58) + (char)rand.Next(48, 58) + (char)rand.Next(48, 58),
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
                customers.Add(c);
            }

            for (int i = 1; i <2; i++)//new parcel
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 6),
                    TargetId = rand.Next(1, 6),
                    Weight = (WeightCategories)rand.Next(0, 3),
                    Priority = (Priorities)rand.Next(0, 3),
                    DroneId = 0,
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.MinValue,
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                if (p.SenderId == p.TargetId)  //to prevent a person sending to himself
                {
                    i--;
                    Config.ParcelId--;
                }
                else
                    parcels.Add(p);
            }
            for (int i = 2; i < 3; i++)//משויכת
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 6),
                    TargetId = rand.Next(1, 6),
                    Weight = (WeightCategories)rand.Next(0, 3),
                    Priority = (Priorities)rand.Next(0, 3),
                    DroneId = i,
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Now.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                if (p.SenderId == p.TargetId)  //to prevent a person sending to himself
                {
                    i--;
                    Config.ParcelId--;
                }
                else
                    parcels.Add(p);
               
            }

            for (int i = 3; i <4; i++)//נאספה ע''י אחפן
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 6),
                    TargetId = rand.Next(1, 6),
                    Weight = (WeightCategories)rand.Next(0, 3),
                    Priority = (Priorities)rand.Next(0, 3),
                    DroneId = i,
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Now.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.Now.AddHours(rand.Next(10, 20)),
                    DeliveredTime = DateTime.MinValue
                };
                if (p.SenderId == p.TargetId)  //to prevent a person sending to himself
                {
                    i--;
                    Config.ParcelId--;
                }
                else
                    parcels.Add(p);
            }
            for (int i = 4; i <6; i++)//סופקה
            {
                Parcel p = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1, 6),
                    TargetId = rand.Next(1, 6),
                    Weight = (WeightCategories)rand.Next(0, 3),
                    Priority = (Priorities)rand.Next(0, 3),
                    DroneId = 0,
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.Now.AddHours(rand.Next(1, 10)),
                    PickedUpTime = DateTime.Now.AddHours(rand.Next(10, 15)),
                    DeliveredTime = DateTime.Now.AddHours(rand.Next(15, 16)),
                };
                if (p.SenderId == p.TargetId)  //to prevent a person sending to himself
                {
                    i--;
                    Config.ParcelId--;
                }
                else
                    parcels.Add(p);
            }
        }
    }
}


