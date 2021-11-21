using System;

namespace ConsoleUI_BL
{
    public class Program
    {
        enum Option { Add, Update, Display, Show, Distance, Exit };//enum  for options
        enum Add { Station, Dron, Customer, Parcel };//enum for add options
        enum Update { Join, Collect, Supply, ChargeDrone, ReleaseDrone };//enum for update options
        enum Display { Station, Drone, Customer, Parcel };//enum for display options
        enum Show { Station, Drone, Customer, Parcel, ParcelWithoutDrone, AvilabaleStations };//enum for show options
        enum Distance { Station, Customer }//enum for distance options
        static void Main(string[] args)
     {
            IBL.IBL ibl = new BL.BL();
          // BLApi.IBL bl = new BL.BLImp();
     }
    }
}     

