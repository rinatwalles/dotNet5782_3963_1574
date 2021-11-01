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

        public DalObject() { DataSource.Initialize(); }
        public void DroneAddition(Drone d)
        {
            DataSource.drones[DataSource.config.droneCounter] = d;
            DataSource.config.droneCounter++;
        }
        public void StationAddition(Station s)
        {
            DataSource.stations[DataSource.config.stationCounter] = s;
            DataSource.config.stationCounter++;
        }
        public void CustomerAddition(Customer c)
        {
            DataSource.customers[DataSource.config.customerCounter] = c;
            DataSource.config.customerCounter++;
        }
        public void ParcelAddition(Parcel p)
        {
            DataSource.parcels[DataSource.config.parcelsCounter] = p;
            DataSource.config.parcelsCounter++;
        }
        public int SearchDrone(int id)
        {
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.drones[i].Id == id)
                    return i;
            }
            return -1;
        }
        public int SearchStation(int id)
        {
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.stations[i].Id == id)
                    return i;
            }
            return -1;
        }

        public int SearchCustomer(int newId)
        {
            for (int i = 0; i < DataSource.config.customerCounter; i++)
            {
                if (DataSource.customers[i].Id == newId)
                    return i;
            }
            return -1;
        }

        public int SearchParcel(int newId)
        {
            for (int i = 0; i < DataSource.config.parcelsCounter; i++)
            {
                if (DataSource.parcels[i].Id == newId)
                    return i;
            }
            return -1;
        }

        public void JoinDroneToParcel(int parcelId)//שיוך חבילה לרחפן
        {
            int index = SearchParcel(parcelId);//חיפוש חבילה עם התז התואם
            for (int i = 0; i < DataSource.config.droneCounter; i++)
            {
                if (DataSource.drones[i].Status == DroneStatuses.Available)
                {
                    DataSource.drones[i].Status = DroneStatuses.Delivery;
                    DataSource.parcels[index].DroneId = DataSource.drones[i].Id;//חיפוש רחפן פנוי לחבילה ושיוך
                    return;
                }
            }
        }
        public void DroneCollecting(int parcelId)//איסוף חבילה ע''י רחפן
        {
            int indexP = SearchParcel(parcelId);//חיפוש חבילה עם התז התואם
            DataSource.parcels[indexP].Scheduled = DateTime.Now;
        }

        public void CustomerCollecting(int parcelId)//איסוף חבילה ע''י לקוח
        {
            int indexP = SearchParcel(parcelId);//חיפוש חבילה עם התז התואם
            DataSource.parcels[indexP].PickedUp = DateTime.Now;
        }

        public void ChargingDrone(int droneId, int stationId)//שליחת רחפן לטעינה
        {
            int indexD = SearchDrone(droneId);
            DataSource.drones[indexD].Status = DroneStatuses.Maintenance;

            DroneCharge dc = new DroneCharge() { DroneId = droneId, StationId = stationId };
            DataSource.droneCharges[DataSource.config.droneChargeCounter] = dc;
            DataSource.config.droneChargeCounter++;

            int indexS = SearchDrone(stationId);
            DataSource.stations[indexS].ChargeSlots--;
            DataSource.drones[indexD].Battery = 100;
        }

        public void ReleaseDrone(int droneId, int stationId)//שליחת רחפן לטעינה
        {
            int indexD = SearchDrone(droneId);
            if (DataSource.drones[indexD].Battery == 100)
            {
                DataSource.drones[indexD].Status = DroneStatuses.Available;
                DataSource.config.droneChargeCounter--;

                int indexS = SearchDrone(stationId);
                DataSource.stations[indexS].ChargeSlots++;
            }
        }

        public void PrintCustomer(int newId)
        {
            int temp = SearchCustomer(newId);
            Console.WriteLine(DataSource.customers[temp]);
        }

        public void PrintAllCustomer()
        {
            for (int i = 0; i < DataSource.config.customerCounter; i++)
                Console.WriteLine(DataSource.customers[i]);
        }

        public void PrintParcel(int newId)
        {
            int temp = SearchParcel(newId);
            Console.WriteLine(DataSource.parcels[temp]);
        }

        public void PrintAllParcels()
        {
            for (int i = 0; i < DataSource.config.parcelsCounter; i++)
                Console.WriteLine(DataSource.parcels[i]);
        }

        public void PrintStation(int newId)
        {
            int temp = SearchStation(newId);
            Console.WriteLine(DataSource.stations[temp]);
        }

        public void PrintAllStation()
        {
            for (int i = 0; i < DataSource.config.stationCounter; i++)
                Console.WriteLine(DataSource.stations[i]);
        }


        public void PrintDrone(int newId)
        {
            int temp = SearchDrone(newId);
            Console.WriteLine(DataSource.drones[temp]);
        }

        public void PrintAllDrones()
        {
            for (int i = 0; i < DataSource.config.droneCounter; i++)
                Console.WriteLine(DataSource.drones[i]);
        }

        public void PrintEmptyCargeSlots()
        {
            for (int i = 0; i < DataSource.config.stationCounter; i++)
            {
                if (DataSource.stations[i].ChargeSlots > 0)
                    Console.WriteLine(DataSource.stations[i]);
            }
        }

        public void ParcelWithoutDrone()
        {
            for (int i = 0; i < DataSource.config.parcelsCounter; i++)
            {
                if (DataSource.parcels[i].DroneId == 0)
                    Console.WriteLine(DataSource.parcels[i]);
            }
        }

        public void DistanceFromStation(int id, double x1, double y1)
        {
            int temp = SearchStation(id);
            double longy = DataSource.stations[temp].Longitude;
            double latx = DataSource.stations[temp].Latitude;
            Console.WriteLine(DistanceCalculate(x1, y1, longy, latx));
        }
        public void DistanceFromCustomer(int id, double x1, double y1)
        {
            int temp = SearchCustomer(id);
            double longy = DataSource.customers[temp].Longitude;
            double latx = DataSource.customers[temp].Latitude;
            Console.WriteLine(DistanceCalculate(x1, y1, longy, latx));
        }

        public double DistanceCalculate(double x1, double y1, double longy, double latx)
        {
            return (Math.Sqrt(Math.Pow(x1-latx, 2)+Math.Pow(y1-longy,2)));
        }
    };
}