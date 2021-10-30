using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

using static DalObject.DataSource;

namespace DalObject
{
    public class DalObject
    {

        static DalObject() { DataSource.Initialize(); }
        public static void droneAddition(Drone d)
        {
            drones[config.droneCounter] = d;
            config.droneCounter++;      
        }
        public static void stationAddition(Station s)
        {
            stations[config.stationCounter] = s;
            config.stationCounter++;
        }
        public static void customerAddition(Customer c)
        {
            customers[config.customerCounter] = c;
            config.customerCounter++;
        }
        public static void parcelAddition(Parcel p)
        {
            parcels[config.parcelsCounter] = p;
            config.parcelsCounter++;
        }
        public static int searchDrone(int id)
        {
            for (int i = 0; i < config.droneCounter; i++)
            {
                if (drones[i].Id == id)
                    return i;
            }
            return -1;
        }
        public static int searchStation(int id)
        {
            for (int i = 0; i < config.droneCounter; i++)
            {
                if (stations[i].Id == id)
                    return i;
            }
            return -1;
        }
        public static void joinDroneToParcel(int parcelId)//שיוך חבילה לרחפן
        {
            int index = searchParcel(parcelId);//חיפוש חבילה עם התז התואם
            for (int i = 0; i < config.droneCounter; i++)
            {
                if (drones[i].Status == DroneStatuses.Available)
                {
                    drones[i].Status = DroneStatuses.Delivery;
                    parcels[index].DroneId = DataSource.drones[i].Id;//חיפוש רחפן פנוי לחבילה ושיוך
                    return;
                }
            }
        }
        public static void droneCollecting(int parcelId)//איסוף חבילה ע''י רחפן
        {
            int indexP = searchParcel(parcelId);//חיפוש חבילה עם התז התואם
            parcels[indexP].Scheduled= DateTime.Now;
        }

        public static void customerCollecting(int parcelId)//איסוף חבילה ע''י לקוח
        {
            int indexP = searchParcel(parcelId);//חיפוש חבילה עם התז התואם
            parcels[indexP].PickedUp = DateTime.Now;
        }


        public static void ChargingDrone(Drone d, Station s, DroneCharge dc)//שליחת רחפן לטעינה
        {
            d.Status = DroneStatuses.Maintenance;
            droneCharges[config.droneChargeCounter] = dc;
            config.droneChargeCounter++;
            s.ChargeSlots--;
            d.Battery = 100;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>>
        /// <param name="s"></param>
        /// <param name="dc"></param>
    public static void ReleaseDrone(Drone d, Station s, DroneCharge dc)//שליחת רחפן לטעינה
    {
        if (d.Battery == 100)
        {
            d.Status = DroneStatuses.Available;
            config.droneChargeCounter--;
            s.ChargeSlots++;
        }
    }

    public static int searchCustomer(int newId)
        {
            for (int i = 0; i < config.customerCounter++; i++)
            {
                if (DataSource.customers[i].Id == newId)
                    return i;
            }
            return -1;
        }

        public static int searchParcel(int newId)
        {
            for (int i = 0; i < config.parcelsCounter++; i++)
            {
                if (DataSource.parcels[i].Id == newId)
                    return i;
            }
            return -1;
        }

        //public static void print<T>(T c)
        // {
        //     Console.WriteLine(c);
        // }

        public static void printCustomer(int newId)
        {
            int temp = searchCustomer(newId);
            Console.WriteLine(DataSource.customers[temp]);
        }

        public static void printAllCustomer()
        {
            for (int i = 0; i < config.customerCounter++; i++)
                Console.WriteLine(DataSource.customers[i]);
        }

        public static void printParcel(int newId)
        {
            int temp = searchParcel(newId);
            Console.WriteLine(DataSource.parcels[temp]);
        }

        public static void printAllParcels()
        {
            for (int i = 0; i < config.parcelsCounter++; i++)
                Console.WriteLine(DataSource.parcels[i]);
        }

        public static void printStation(int newId)
        {
            int temp = searchStation(newId);
            Console.WriteLine(DataSource.stations[temp]);
        }

        public static void printAllStation()
        {
            for (int i = 0; i < config.stationCounter++; i++)
                Console.WriteLine(DataSource.stations[i]);
        }


        public static void printDrone(int newId)
        {
            int temp = searchDrone(newId);
            Console.WriteLine(DataSource.drones[temp]);
        }

        public static void printAllDrones()
        {
            for (int i = 0; i < config.droneCounter++; i++)
                Console.WriteLine(DataSource.drones[i]);
        }

        public static void printEmptyCargeSlots()
        {
            for (int i = 0; i < config.stationCounter++; i++)
            { 
                if(DataSource.stations[i].ChargeSlots>0)
                    Console.WriteLine(DataSource.stations[i]);
            }
        }

        public static void ParcelWithoutDrone()
        {
            for (int i = 0; i < config.parcelsCounter++; i++)
            {
                if (DataSource.parcels[i].DroneId == 0)
                    Console.WriteLine(DataSource.parcels[i]);
            }
        }

    }
}

