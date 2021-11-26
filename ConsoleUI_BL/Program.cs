using IBL.BO;
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
            // קודם כל מיקום בשביל כמה אוביקטים
            IBL.BO.Location locate = new IBL.BO.Location()
            {
                Longitude = 0, Latitude = 0
            };
            //קליטת תחנת בסיס,
            Console.WriteLine("Insert id, name, AvilableChargeSlotsNumber");
            IBL.BO.BaseStation sstation = new IBL.BO.BaseStation()
            {
                Id = int.Parse(System.Console.ReadLine()),
                Name = System.Console.ReadLine(),
                AvilableChargeSlotsNumber = int.Parse(System.Console.ReadLine()),
                DroneCharging = new System.Collections.Generic.List<DroneCharging>()//בטוח צריך?זה זה מה שהתכוונו?
            };
            locate.Longitude = double.Parse(Console.ReadLine());
            locate.Latitude = double.Parse(Console.ReadLine());
            sstation.SLocation = locate;
            ibl.AddBaseStation(sstation);
            //קליטת רחפן
            Console.WriteLine("Insert id, model, maximum wight");
            IBL.BO.Drone ddrone = new IBL.BO.Drone()
            {
                Id = int.Parse(System.Console.ReadLine()),
                Model = System.Console.ReadLine(),
                Weight = (WeightCategories)System.Console.Read(),
            };
            int sId = int.Parse(System.Console.ReadLine());
            ibl.AddDrone(ddrone, sId);
            //קליטת לקוח
            Console.WriteLine("Insert id, name, number, location");
            IBL.BO.Customer ccustomer = new IBL.BO.Customer()
            {
                Id = int.Parse(System.Console.ReadLine()),
                Name = System.Console.ReadLine(),
                Phone = System.Console.ReadLine(),
            };
            locate.Longitude = double.Parse(Console.ReadLine());
            locate.Latitude = double.Parse(Console.ReadLine());
            ccustomer.Place = locate;
            ibl.AddCustomer(ccustomer);
            //קליטת חבילה
            Console.WriteLine("Insert id, name, number, location");
            IBL.BO.Parcel pparcel = new IBL.BO.Parcel()
            {
                //לעשות תת תפריט למשקל ועדיפות
            };
            int IdSender = int.Parse(System.Console.ReadLine());
            int IdReceiver = int.Parse(System.Console.ReadLine());
            ibl.AddParcel(pparcel, IdSender, IdReceiver);


            // אפשרויות עדכון

            //עדכון רחפן
            Console.WriteLine("Insert id of drone and its new name");
            int idS = int.Parse(System.Console.ReadLine());
            string modelS = System.Console.ReadLine();
            ibl.UpdateDrone(idS, modelS);

            //update customer
            Console.WriteLine("Insert id of drone and its new name");
            int idC = int.Parse(System.Console.ReadLine());
            string nameC = System.Console.ReadLine();
            string phoneC = System.Console.ReadLine();
            ibl.UpdateCustomer(idC,nameC, phoneC);
        }
    }
}     

