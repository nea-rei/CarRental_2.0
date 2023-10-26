using Common.Enums;
using Common.Interfaces;

namespace Data.Interfaces;

public interface IData
{
    List<T> Get<T>(Func<T, bool>? lambda = null);
    T? Single<T>(Func<T, bool>? lambda);
    void Add<T>(T item);

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    public string[] VehicleStatusNames { get; }
    public string[] VehicleTypeNames { get; }
    VehicleType GetVehicleType(string name);
    int GetDailyCost(VehicleType type);

    IBooking RentVehicle(int vehicleId, int customerId);
    IBooking ReturnVehicle(int vehicleId);
}
