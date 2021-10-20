using System;

namespace IDAL
{
    namespace DO
    {
        struct Station
        {
            public int Id { get; set;}
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }
        }

        struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
        }

        struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
        }
        struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

    }

}
    

