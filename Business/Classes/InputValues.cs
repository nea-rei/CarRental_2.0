using Common.Classes;
using Common.Enums;
using Common.Interfaces;

namespace Business.Classes;

public class InputValues
{
    public string vehicletype = string.Empty;
    public string customer = string.Empty;
    public VehicleStatus vehicleStatus = VehicleStatus.Available;

    public Customer c = new();
    public Car v = new();
    public Booking b = new();

    public void Clear()
    {
        c.SSN = string.Empty;
        c.LastName = string.Empty;
        c.FirstName = string.Empty;
        v.RegNo = string.Empty;
        v.Brand = string.Empty;
        v.Odometer = 0;
        v.CostKm = 0;
        vehicletype = string.Empty;
        customer = string.Empty;
    }
}
