using Common.Enums;

namespace Common.Interfaces
{
    public interface IVehicle
    {
        public int Id { get; }
        public string RegNo { get; }
        public string Brand { get; }
        public double Odometer { get; set; }
        public double CostKm { get; set; }
        public double DailyCost { get; set; }
        public VehicleType VType { get; set; }
        public VehicleStatus Status { get; set; }

        void AssignId(int id);
        void AssignDailyCost(double dailycost);
    }
}
