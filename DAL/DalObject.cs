using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
using static DalObject.DataSource;

namespace DAL
{
    namespace DalObject
    {
        public class DalObject
        {
            static DalObject() { DataSource.Initialize(); }
            public void droneAddition(int newId, WeightCategories newMaxWeight, DroneStatuses newStatus, double newBattery)
            {
                DataSource.drones[DataSource.config.droneCounter] = new Drone
                {
                    Id = newId,
                    //איתחול מודלים סמהאו
                    MaxWeight = newMaxWeight,
                    Status = newStatus,
                    Battery = newBattery//לא יתן 1, רק קטן מ1!
                };
            }
            public void stationAddition(int newId, double newLongitude, double newLatitude, int newChargeSlots)
            {
                DataSource.stations[DataSource.config.stationCounter] = new Station
                {
                    Id = newId,
                    //איתחול מודלים סמהאו
                    //אתחול שם string
                    Longitude = newLongitude,
                    Latitude = newLatitude,
                    ChargeSlots = newChargeSlots//יצטרכו לשנות 
                };
            }
            public void customerAddition(int newId, double newLongitude, double newLatitude)
            {
                DataSource.customers[DataSource.config.customerCounter] = new Customer
                {
                    Id = newId,
                    //איתחול ניימס 
                    //איתחול טלפונים
                    Longitude = newLongitude,
                    Latitude = newLatitude,
                };
            }
            public void parcelAddition(int newId, int newSenderId, int newTargetId, WeightCategories newWeight, Priorities newPriorities, DateTime newRequested, int NewDroneId, DateTime newScheduled, DateTime newPickedUp, DateTime newDelivered)
            {
                DataSource.parcels[DataSource.config.parcelsCounter] = new Parcel
                {
                    Id = newId,
                    SenderId = newSenderId,
                    TargetId = newTargetId,
                    Weight = newWeight,
                    Priority = newPriorities,
                    Requested = newRequested,//איתחול ריקוסטד
                    DroneId = NewDroneId,
                    Scheduled = newScheduled,//איתחול סקדולד
                    PickedUp = newPickedUp,//איתחול פיקד אפ
                    Delivered = newDelivered,//איתחול דליברד
                };
            }
            public int searchDrone(int id)
            {
                for (int i = 0; i < config.droneCounter; i++)
                {
                    if (DataSource.drones[i].Id == id)
                        return i;
                }
                return -1;
            }
            public int searchStation(int id)
            {
                for (int i = 0; i < config.droneCounter; i++)
                {
                    if (DataSource.stations[i].Id == id)
                        return i;
                }
                return -1;
            }
            public void joinDroneToParcel(int id)//שיוך חבילה לרחפן
            {
                int index = searchParcel(id);//חיפוש חבילה עם התז התואם
                for (int i = 0; i < DataSource.drones.Length; i++)
                {
                    if (DataSource.drones[i].Status == DroneStatuses.Available)
                    {
                        DataSource.drones[i].Status = DroneStatuses.Delivery;
                        DataSource.parcels[index].DroneId = DataSource.drones[i].Id;//חיפוש רחפן פנוי לחבילה ושיוך
                        return;
                    }
                }
            }
            public void droneCollecting(int id)//איסוף חבילה ע''י רחפן
            {
                int indexP = searchParcel(id);//חיפוש חבילה עם התז התואם
                DataSource.parcels[indexP].Scheduled= DateTime.Now;
            }

            public void customerCollecting(int id)//איסוף חבילה ע''י לקוח
            {
                int indexP = searchParcel(id);//חיפוש חבילה עם התז התואם
                DataSource.parcels[indexP].PickedUp = DateTime.Now;
            }


            public void chargingDrone(int parcelId, int stationId)
            {
                int indexP = searchParcel(parcelId);//חיפוש חבילה עם התז התואם
               
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

            public int searchParcel(int newId)
            {
                for (int i = 0; i < config.parcelsCounter++; i++)
                {
                    if (DataSource.parcels[i].Id == newId)
                        return i;
                }
                return -1;
            }


            public void printCustomer(int newId)
            {
                int temp = searchCustomer(newId);
                Console.WriteLine(DataSource.customers[temp]);
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
}
