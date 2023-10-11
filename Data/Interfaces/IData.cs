using Common.Enums;
using Common.Interfaces;

namespace Data.Interfaces;

public interface IData
{
    IEnumerable<IPerson> GetPersons();
    IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default);
    IEnumerable<IBooking> GetBookings();
}
