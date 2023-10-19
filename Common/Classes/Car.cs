using Common.Enums;
using Common.Interfaces;

namespace Common.Classes;

public class Car : Vehicle
{
    public Car(int id, string regno, string brand, double odometer, double costkm,
        double dailycost, VehicleType vtype, VehicleStatus status ) :
        base(id, regno, brand, odometer, costkm, dailycost, vtype, status) {}
    public Car(string regno, string brand, double odometer, double costkm,
        VehicleType vtype, VehicleStatus status) :
        base(regno, brand, odometer, costkm, vtype, status) { }
}
