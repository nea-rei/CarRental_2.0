using Common.Enums;
using Common.Interfaces;

namespace Common.Classes;

public abstract class Vehicle : IVehicle
{
    public int Id { get; private set; }
    public string RegNo { get; init;  } = string.Empty;
    public string Brand { get; init; } = string.Empty;
    public double Odometer { get; set; }
    public double CostKm { get; init; }
    public double DailyCost { get; private set; }
    public VehicleType VType { get; init; }
    public VehicleStatus Status { get; set; } = default;

    public void AssignId(int id) => Id = id;
    public void AssignDailyCost(double dailycost) => DailyCost = dailycost;
    public void UpdateStatus(VehicleStatus status) => Status = status;

    protected Vehicle(int id, string regno, string brand, double odometer,
        double costkm, double dailycost, VehicleType vtype, VehicleStatus status)
    {
        Id = id;
        RegNo = regno;
        Brand = brand;
        Odometer = odometer;
        CostKm = costkm;
        DailyCost = dailycost;
        Status = status;
        VType = vtype;
        Status = status;
    }
    protected Vehicle(string regno, string brand, double odometer,
        double costkm, VehicleType vtype, VehicleStatus status)
    {
        RegNo = regno;
        Brand = brand;
        Odometer = odometer;
        CostKm = costkm;
        Status = status;
        VType = vtype;
        Status = status;
    }

}
