using Common.Enums;

namespace Common.Interfaces;

public interface IVehicle
{
    public string RegNo { get; init; }
    public string Brand { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleType VType { get; set; }
    public int DailyCost { get; set; }
    public VehicleStatus Status { get; set; }

}
