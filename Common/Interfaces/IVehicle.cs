using Common.Enums;

namespace Common.Interfaces
{
    public interface IVehicle
    {
        public int Id { get; }
        public string RegNo { get; init; }
        public string Brand { get; init; }
        public double Odometer { get; set; }
        public double CostKm { get; init; }
        public double DailyCost { get; }
        public VehicleType VType { get; init; }
        public VehicleStatus Status { get; set; }

        void AssignId(int id);
        void AssignDailyCost(double dailycost);
        void UpdateStatus(VehicleStatus status);
    }
}
