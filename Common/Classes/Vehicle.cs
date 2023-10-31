using Common.Enums;
using Common.Extensions;
using Common.Interfaces;

namespace Common.Classes;

public abstract class Vehicle : IVehicle
{
    public int Id { get; private set; }
    public string RegNo { get; set;  } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public double Odometer { get; set; }
    public double CostKm { get; set; }
    public double DailyCost { get; private set; }
    public VehicleType VType { get; set; }
    public VehicleStatus Status { get; set; } = default;

    public void AssignId(int id) => Id = id;
    public void UpdateStatus(VehicleStatus status) => Status = status;

    public void AssignDailyCost(VehicleType type)
    {
        DailyCost = type.GetDailyCost();
    }
    public Vehicle() { }
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
