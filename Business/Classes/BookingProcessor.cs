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

    public string error = string.Empty;

    /*CUSTOMER*/
    public string ssn = string.Empty;
    public string lastname = string.Empty;
    public string firstname = string.Empty;

    /*BOOKING*/
    public string customer = string.Empty;
    public string vehicle = string.Empty;
    public double distance;
    public bool rent = false;

    /*VEHICLE*/
    public double odometer;
    public double costkm;
    public double dailycost;
    public string regno = string.Empty;
    public string brand = string.Empty;
    public string type = string.Empty;

    public VehicleStatus vehicleStatus = VehicleStatus.Available;

    public string[] VehicleStatusNames => _data.VehicleStatusNames;
    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleType GetVehicleType(string name) => _data.GetVehicleType(name);
    public VehicleType vehicleType => GetVehicleType(type);
    public IPerson? person => GetPerson(customer);

    public BookingProcessor(IData data) => _data = data;

    /*GET_METODER*/

    public IEnumerable<IVehicle> GetVehicles() => _data.Get<IVehicle>();
    public IEnumerable<IPerson> GetCustomers() => _data.Get<IPerson>();
    public IEnumerable<IBooking> GetBookings() => _data.Get<IBooking>();

    public IBooking? GetBooking(int vehicleId) => _data.Single<IBooking>(b => b.Id == vehicleId);
    public IPerson? GetPerson(string ssn) => _data.Single<IPerson>(p => p.SSN == ssn);
    public IVehicle? GetVehicle(int vehicleId) => _data.Single<IVehicle>(v => v.Id == vehicleId);
    public IVehicle? GetVehicle(string regno) => _data.Single<IVehicle>(v => v.RegNo == regno);

    /*ADD_METODER*/
    public void AddVehicle(string regno, string brand,
        double odometer, double costkm, VehicleType vtype, VehicleStatus status)
    {
        try
        {
            if (regno.Length < 5 || brand.Length < 5 || odometer < 100 || costkm < 10 || type.Equals(null))
                throw new ArgumentException(error);

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
            if (ssn.Length < 5 || lastname.Length < 2 || lastname.Length < 2)
                throw new ArgumentException(error);

            error = string.Empty;
            rent = true;

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
    ////public async Task<IBooking> RentVehicle(int vehicleId, int customerId)
    //{
    //    try
    //    {
    //        if (vehicleId < 1 || customerId < 1) throw new ArgumentException(error);

    //        rent = true;
    //        error = string.Empty;

    //        //customerId = Convert.ToInt32(customer);
    //        //var person = GetPerson(customerId);

    //        //var booking = new Booking() { RentalDate = DateTime.UtcNow };
    //        //var id = _data.NextBookingId;
    //        //booking.AssignId(id);

    //        //await Task.Delay(10000);
    //        //_data.Add<IBooking>(booking);
    //        //return booking;
    //    }
    //    catch
    //    {
    //        error = "Could not add booking";
    //    }
    //}

    //public IBooking ReturnVehicle(int vehicleId, double distance) { return }





}
