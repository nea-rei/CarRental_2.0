using Common.Enums;

namespace Common.Interfaces
{
    public interface IVehicle
    {
        public int Id { get; }
        public string RegNo { get; set; }
        public string Brand { get; set; }
        public double Odometer { get; set; }
        public double CostKm { get; set; }
        public double DailyCost { get; }
        public VehicleType VType { get; set; }
        public VehicleStatus Status { get; set; }

        void AssignId(int id);
        void UpdateStatus(VehicleStatus status);
        public void AssignDailyCost(VehicleType type);
    }
}
