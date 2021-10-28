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
        static DalObject() { Initialize(); }
        public void droneAddition(Drone d)
        {
            drones[config.droneCounter] = d;
            config.droneCounter++;      
        }
        public void stationAddition(Station s)
        {
            stations[config.stationCounter] = s;
            config.stationCounter++;
        }
        public void customerAddition(Customer c)
        {
            customers[config.customerCounter] = c;
            config.customerCounter++;
        }
        public void parcelAddition(Parcel p)
        {
            parcels[config.parcelsCounter] = p;
            config.parcelsCounter++;
        }
        public int searchDrone(int id)
        {
            for (int i = 0; i < config.droneCounter; i++)
            {
                if (drones[i].Id == id)
                    return i;
            }
            return -1;
        }
        public int searchStation(int id)
        {
            for (int i = 0; i < config.droneCounter; i++)
            {
                if (stations[i].Id == id)
                    return i;
            }
            return -1;
        }
        public void joinDroneToParcel(Drone d)//שיוך חבילה לרחפן
        {
            int index = searchParcel(d.Id);//חיפוש חבילה עם התז התואם
            for (int i = 0; i < drones.Length; i++)
            {
                if (drones[i].Status == DroneStatuses.Available)
                {
                    drones[i].Status = DroneStatuses.Delivery;
                    parcels[index].DroneId = DataSource.drones[i].Id;//חיפוש רחפן פנוי לחבילה ושיוך
                    return;
                }
            }
        }
        public static void droneCollecting(Drone d)//איסוף חבילה ע''י רחפן
        {
            int indexP = searchParcel(d.Id);//חיפוש חבילה עם התז התואם
            parcels[indexP].Scheduled= DateTime.Now;
        }

        public static void customerCollecting(Parcel p)//איסוף חבילה ע''י לקוח
        {
            int indexP = searchParcel(p.Id);//חיפוש חבילה עם התז התואם
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

    public int searchCustomer(int newId)
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
       

    public void print<T>(T c)
        {
            Console.WriteLine(c);
        }

        public void printAllCustomer()
        {
            for (int i = 0; i < config.customerCounter++; i++)
                Console.WriteLine(DataSource.customers[i]);
        }



        public void printParcel(int newId)
        {
            int temp = searchParcel(newId);
            Console.WriteLine(DataSource.parcels[temp]);
        }

        public void printAllParcels()
        {
            for (int i = 0; i < config.parcelsCounter++; i++)
                Console.WriteLine(DataSource.parcels[i]);
        }



        public void printStation(int newId)
        {
            int temp = searchStation(newId);
            Console.WriteLine(DataSource.stations[temp]);
        }

        public void printAllStation()
        {
            for (int i = 0; i < config.stationCounter++; i++)
                Console.WriteLine(DataSource.stations[i]);
        }


        public void printDrones(int newId)
        {
            int temp = searchDrone(newId);
            Console.WriteLine(DataSource.drones[temp]);
        }

        public void printAllDrones()
        {
            for (int i = 0; i < config.droneCounter++; i++)
                Console.WriteLine(DataSource.drones[i]);
        }

        public void printEmptyCargeSlots()
        {
            for (int i = 0; i < config.stationCounter++; i++)
            { 
                if(DataSource.stations[i].ChargeSlots>0)
                    Console.WriteLine(DataSource.stations[i]);
            }
        }

    }
}

