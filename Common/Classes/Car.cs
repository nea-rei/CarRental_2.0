using Common.Enums;
using Common.Interfaces;

namespace Common.Classes;

public class Car : IVehicle
{
    public string RegNo { get; init; } = string.Empty;
    public string Brand { get; init; } = string.Empty;
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleType VType { get; set; }
    public int DailyCost { get; set; }
    public VehicleStatus Status { get; set; }



    public Car(string regNo, string brand, int odometer, double costKm, VehicleType vType,
        int dailyCost, VehicleStatus status)
    {
        RegNo = regNo;
        Brand = brand;
        Odometer = odometer;
        CostKm = costKm;
        VType = vType;
        DailyCost = dailyCost;
        Status = status;
    }
}
