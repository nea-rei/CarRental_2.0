using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;

namespace Business.Classes;

public class BookingProcessor
{
    private readonly IData _data;

    public VehicleStatus _vehicleStatus = VehicleStatus.Available;
    public BookingProcessor(IData data) => _data = data;
    public IEnumerable<IPerson> Customers => _data.GetPersons();
    public IEnumerable<IVehicle> Vehicles => _data.GetVehicles();
    public IEnumerable<IBooking> Bookings => _data.GetBookings();
}
