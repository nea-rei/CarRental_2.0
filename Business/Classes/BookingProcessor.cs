using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Business.Classes;

public class BookingProcessor
{
    private readonly IData _data;

    public string ssn = string.Empty;
    public string lastname = string.Empty;
    public string firstname = string.Empty;
    public string customer = string.Empty;
    public string type = string.Empty;


    public double odometer;
    public double costkm;
    public double dailycost;
    public double distance;

    public string regno = string.Empty;
    public string brand = string.Empty;
    public string error = string.Empty;


    public VehicleStatus vehicleStatus = VehicleStatus.Available;

    public string[] VehicleStatusNames => _data.VehicleStatusNames;
    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleType GetVehicleType(string name) => _data.GetVehicleType(name);
    public VehicleType vehicleType => GetVehicleType(type);

    public BookingProcessor(IData data) => _data = data;

    /*GET_METODER*/

    public IEnumerable<IVehicle> GetVehicles() => _data.Get<IVehicle>();
    public IEnumerable<IPerson> GetCustomers() => _data.Get<IPerson>();
    public IEnumerable<IBooking> GetBookings() => _data.Get<IBooking>();

    public IBooking? GetBooking(int vehicleId) => _data.Single<IBooking>(b => b.Id == vehicleId);
    public IPerson? GetPerson(string ssn) => _data.Single<IPerson>(p => p.SSN == ssn);
    public IVehicle? GetVehicle(int vehicleId) => _data.Single<IVehicle>(v => v.Id == vehicleId);
    public IVehicle? GetVehicle(string regno) => _data.Single<IVehicle>(v => v.RegNo == regno);

    public void AddVehicle(string regno, string brand,
        double odometer, double costkm, VehicleType vtype, VehicleStatus status)
    {
        try
        {
            error = string.Empty;

            if (vtype.Equals(VehicleType.Motorcycle))
            {
                var motorcycle = new Motorcycle(regno, brand, odometer, costkm, vtype, status);

                var dailycost = _data.GetDailyCost(vtype);
                motorcycle.AssignDailyCost(dailycost);

                var id = _data.NextVehicleId;
                motorcycle.AssignId(id);

                _data.Add<IVehicle>(motorcycle);
            }
            else
            {
                var car = new Car(regno, brand, odometer, costkm, vtype, status);

                var dailycost = _data.GetDailyCost(vtype);
                car.AssignDailyCost(dailycost);

                var id = _data.NextVehicleId;
                car.AssignId(id);
                _data.Add<IVehicle>(car);
            }
        }
        catch
        {
            error = "Could not add vehicle";
        }
    }
    public void AddCustomer(string ssn, string lastname, string firstname)
    {
        try
        {
            error = string.Empty;

            var customer = new Customer(ssn, lastname, firstname);
            var id = _data.NextPersonId;
            customer.AssignId(id);
            _data.Add<IPerson>(customer);

            ssn = string.Empty;
            lastname = string.Empty;
            firstname = string.Empty;

        }
        catch
        {
            error = "Could not add customer";
        }

    }
    //public async Task<IBooking> RentVehicle(int vehicleId, int customerId)
    //{
            //disable knappar för vehicle-tabellen
            //await Task.Delay(10000);
    //
    //    var booking = new Booking();

    //    var id = _data.NextBookingId;
    //    booking.AssignId(id);
    //    _data.Add<IBooking>(booking);

    //
    //    return booking;
    //}

    //public IBooking ReturnVehicle(int vehicleId, double distance) { return }





}
