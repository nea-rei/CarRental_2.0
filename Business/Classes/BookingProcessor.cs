using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;

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
    public string _distance = string.Empty;
    public bool processing = false;
    public int customerId;

    /*VEHICLE*/
    public double odometer;
    public double costkm;
    public double dailycost;
    public string regno = string.Empty;
    public string brand = string.Empty;
    public string? vehicletype = string.Empty;

    public VehicleStatus vehicleStatus = VehicleStatus.Available;

    public string[] VehicleStatusNames => _data.VehicleStatusNames;
    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleType GetVehicleType(string name) => _data.GetVehicleType(name);

    public BookingProcessor(IData data) => _data = data;

    public IEnumerable<IVehicle> GetVehicles() => _data.Get<IVehicle>();
    public IEnumerable<IPerson> GetCustomers() => _data.Get<IPerson>();
    public IEnumerable<IBooking> GetBookings() => _data.Get<IBooking>();

    public IBooking? GetBooking(int vehicleId) => _data.Single<IBooking>(v => v.Vehicle.Id == vehicleId);
    public IPerson? GetPerson(string ssn) => _data.Single<IPerson>(p => p.SSN == ssn);

    public IVehicle? GetVehicle(int vehicleId) => _data.Single<IVehicle>(v => v.Id == vehicleId);
    public IVehicle? GetVehicle(string regno) => _data.Single<IVehicle>(v => v.RegNo == regno);


    public void Clear()
    {
        ssn = string.Empty;
        lastname = string.Empty;
        firstname = string.Empty;
        odometer = 0;
        costkm = 0;
        regno = string.Empty;
        brand = string.Empty;
        vehicletype = string.Empty;
        _distance = string.Empty;
        customer = string.Empty;
        customerId = 0;
    }
    public void AddVehicle(string regno, string brand,
        double odometer, double costkm, string vehicletype, VehicleStatus status)
    {
        try
        {
            if (regno.Length < 5 || brand.Length < 4 || odometer < 100 || costkm == 0 || vehicletype.Equals(string.Empty))
                throw new ArgumentException(error);

            error = string.Empty;

            VehicleType vtype = GetVehicleType(vehicletype);

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
            error = "could not add vehicle";
        }
        Clear();
    }
    public void AddCustomer(string ssn, string lastname, string firstname)
    {
        try
        {
            if (ssn.Length < 5 || lastname.Length < 2 || lastname.Length < 2)
                throw new ArgumentException(error);

            error = string.Empty;

            var customer = new Customer(ssn, lastname, firstname);
            var id = _data.NextPersonId;
            customer.AssignId(id);
            _data.Add<IPerson>(customer);
        }
        catch
        {
            error = "could not add customer";
        }
        Clear();
    }
    public async Task<IBooking?> RentVehicle(int vehicleId, int customerId)
    {
        IBooking? booking;
        IBooking? nobooking = default;
        error = string.Empty;

        try
        {
            customerId = Convert.ToInt32(customer);
            if (vehicleId < 1 || customerId < 1) throw new ArgumentException(error);
            processing = true;
            await Task.Delay(3000);
            booking = await Task.Run(() => _data.RentVehicle(vehicleId, customerId));
            processing = false;
            Clear();
            return booking ?? throw new Exception(error);
        }
        catch
        {
            error = "you must choose a customer";
            return nobooking;
        }

    }
    public IBooking ReturnVehicle(int vehicleId, string ddistance)
    {
        IBooking booking;

        if (vehicleId < 1) throw new ArgumentException("Could not find vehicle id");
        booking = _data.ReturnVehicle(vehicleId);

        try
        {
            double distance = Convert.ToDouble(ddistance);
            if (ddistance.Length < 0 || distance == 0) throw new ArgumentException(error);

            error = string.Empty;

            if (booking is not null)
            {
                booking.ReturnedKm = (distance + booking.Vehicle.Odometer);
                booking.ReturnDate = DateTime.Now;
                booking.Status = VehicleStatus.Available;
                booking.ReturnVehicle(booking.Vehicle);
                booking.StartKm = (double)(booking.ReturnedKm - distance);
            }

            UpdateVehicle(vehicleId, distance);
            Clear();
            return booking ?? throw new Exception("Could not return vehicle");
        }
        catch
        {
            error = "please fill in distance";
            return booking;
        }
    }

    public IVehicle UpdateVehicle(int vehicleId, double distance)
    {
        var vehicle = GetVehicle(vehicleId);
        try
        {
            if (vehicle is null || distance == 0) throw new ArgumentException(error);
            error = string.Empty;

            if (vehicle is not null)
            {
                vehicle.Odometer += distance;
                vehicle.Status = VehicleStatus.Available;
            }
        }
        catch
        {
            error = "could not update vehicle";
        }
        return vehicle ?? throw new ArgumentException(error);
    }
}
