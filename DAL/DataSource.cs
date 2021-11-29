using System;
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
        internal class Config
        {
            static public int ParcelId = 1;
            static public double Availavble = 0.1;
            static public double Light = 0.3;
            static public double Medium =0.4;
            static public double Heavy= 0.5;
            static public double ChargePrecent= 0.35;  //קצב טעינת רחפן
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
            for (int i = 1; i <= 5; i++)
            {
                Drone d = new Drone ()
                {
                    Id =i,
                    Model = "abc",
                    Weight = (WeightCategories)rand.Next(0, 2),
                   // Status = (DroneStatuses)rand.Next(0, 1),
                   // Battery = rand.NextDouble()*100//לא יתן 1, רק קטן מ1!
                };
                drones.Add(d);
            }

            for (int i = 1; i <= 2; i++)
            {
                Station s = new Station()
                {
                    Id = i,
                    Name = "ghi",
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                    AvailableChargeSlots = rand.Next(0, 10)   //number of empty charge slots in each station
                };
                stations.Add(s);
            }

            for (int i = 1; i <=10; i++)
            {
                Customer c = new Customer()
                {
                    Id = i,
                    Name ="def",
                    Phone= "12345678",
                    Longitude = rand.NextDouble() * (33.5 - 29.3) + 29.3,
                    Latitude = rand.NextDouble() * (36.3 - 33.7) + 33.7,
                };
                customers.Add(c);
            }

            for (int i = 0; i < 10; i++)
            {
               Parcel p= new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1,10),
                    TargetId = rand.Next(1, 10),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(1, 5),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = DateTime.MinValue,
                    PickedUpTime = DateTime.MinValue,
                    DeliveredTime = DateTime.MinValue
                };
                parcels.Add(p);
            }

        }

    }
}

