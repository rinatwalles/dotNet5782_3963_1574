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
            static public int ParcelId = 0;
            static public double Availavble { get; set; }
            static public double Light { get; set; }
            static public double Medium { get; set; }
            static public double Heavy { get; set; }
            static public double ChargePrecent { get; set; }   //קצב טעינת רחפן
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
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
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
                parcels[i] = new Parcel()
                {
                    Id = Config.ParcelId++,
                    SenderId = rand.Next(1000, 2000),
                    TargetId = rand.Next(1000, 2000),
                    Weight = (WeightCategories)rand.Next(0, 2),
                    Priority = (Priorities)rand.Next(0, 2),
                    DroneId = rand.Next(10, 20),
                    RequestedTime = DateTime.Now,
                    ScheduledTime = parcels[i].RequestedTime.AddHours(1),
                    PickedUpTime = parcels[i].RequestedTime.AddHours(1),
                    DeliveredTime = parcels[i].RequestedTime.AddHours(1),
                };
            }

        }

    }
}

