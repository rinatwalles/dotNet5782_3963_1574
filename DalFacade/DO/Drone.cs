using System;


namespace DO
{
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        //public DroneStatuses Status { get; set; }
        // public double Battery { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }


}






