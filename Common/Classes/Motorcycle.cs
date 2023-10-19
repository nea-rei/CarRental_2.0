using Common.Enums;
using Common.Interfaces;

namespace Common.Classes;

public class Motorcycle : Vehicle
{
    public Motorcycle(int id, string regno, string brand, double odometer, double costkm,
       double dailycost, VehicleType vtype, VehicleStatus status) :
       base(id, regno, brand, odometer, costkm, dailycost, vtype, status) { }
    public Motorcycle(string regno, string brand, double odometer, double costkm,
         VehicleType vtype, VehicleStatus status) :
        base(regno, brand, odometer, costkm, vtype, status) { }
}