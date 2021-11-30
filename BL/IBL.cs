﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        #region Add
        void AddDrone(Drone ddrone, int sId);
        void AddBaseStation(Station s);
        void AddCustomer(Customer c);
        void AddParcel(Parcel p, int IdSender,  int IdReceiver);
        Location MinDistanceOfSation(Location locat);

        #endregion

        #region Upadste
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name="", int allChargingPositions=0);
        void UpdateCustomer(int id, string name="", string phone="");
        #endregion

        #region Show
        IEnumerable<BaseStationToList> GetAllStations();
        IEnumerable<DroneToList> GetAllDrones();
        IEnumerable<CustomerToList> GetAllCustomers();
        IEnumerable<ParcelToList> GetAllParcels();
        #endregion

        #region Display
        CustomerOfParcel getCustomerOfParcel(int id);
        Drone getDrone(int id);
        Station getBaseStation(int id);
        Customer GetCustomer(int id);
        Parcel getParcel(int id);
        #endregion
    }
}
