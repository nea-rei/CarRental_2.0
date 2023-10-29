using Common.Enums;

namespace Common.Extensions
{
    public static class VehicleExtensions
    {
        public static double GetDailyCost(this VehicleType type) =>
        type switch
        {
            VehicleType.Sedan => 100,
            VehicleType.Van => 300,
            VehicleType.Combi => 200,
            VehicleType.Motorcycle => 50,
            _ => throw new ArgumentException("Vehicletype does not exist"),
        };
    }
}
