using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using System.Linq.Expressions;

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
    public VehicleType GetVehicleType(string name);
    public int GetDailyCost(VehicleType type);

    IBooking RentVehicle(int vehicleId, int customerId);
    IBooking ReturnVehicle(int vehicleId);
}
