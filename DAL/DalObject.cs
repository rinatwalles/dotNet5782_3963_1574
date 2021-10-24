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
            public void joinDroneToParcel(int Id)
            {
                
            }
        }
    }
}
