﻿using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;

namespace Business.Classes;

public class BookingProcessor
{
    private readonly IData _data;
    public InputValues _in;

    public string error = string.Empty;
    public bool processing = false;
    public string _distance = string.Empty;

    public string[] VehicleStatusNames => _data.VehicleStatusNames;
    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleType GetVehicleType(string name) => _data.GetVehicleType(name);

    public BookingProcessor(IData data, InputValues inputValues) => (_data, _in) = (data, inputValues);

    public IEnumerable<IVehicle> GetVehicles() => _data.Get<IVehicle>();
    public IEnumerable<IVehicle> GetVehicles(string regno) => _data.Get<IVehicle>(v => v.RegNo == regno);
    public IEnumerable<IPerson> GetCustomers() => _data.Get<IPerson>();
    public IEnumerable<IPerson> GetCustomers(string ssn) => _data.Get<IPerson>(p => p.SSN == ssn);
    public IEnumerable<IBooking> GetBookings() => _data.Get<IBooking>();

    public IVehicle? GetVehicle(int vehicleId) => _data.Single<IVehicle>(v => v.Id == vehicleId);
    public IVehicle? GetVehicle(string regno) => _data.Single<IVehicle>(v => v.RegNo == regno);
    public void Clear()
    {
        _distance = string.Empty;
    }
    public void AddVehicle(string regno, string brand,
        double odometer, double costkm, string vehicletype, VehicleStatus status)
    {
        try
        {
            if (regno.Length < 1 || brand.Length < 1 || odometer == 0 || costkm == 0 || vehicletype.Equals(string.Empty))
                throw new ArgumentException(error);

            var collection = GetVehicles(regno).ToList();

            if (collection.Count > 0) throw new ArgumentNullException(error);
                error = string.Empty;

            VehicleType vtype = GetVehicleType(_in.vehicletype);

            if (vtype.Equals(VehicleType.Motorcycle))
            {
                var motorcycle = new Motorcycle(regno, brand, odometer, costkm, vtype, status);

                motorcycle.AssignDailyCost(vtype);

                var id = _data.NextVehicleId;
                motorcycle.AssignId(id);

                _data.Add<IVehicle>(motorcycle);
            }
            else
            {
                var car = new Car(regno, brand, odometer, costkm, vtype, status);

                car.AssignDailyCost(vtype);

                var id = _data.NextVehicleId;
                car.AssignId(id);
                _data.Add<IVehicle>(car);
            }
        }
        catch
        {
            error = "could not add vehicle";
        }
        _in.Clear();
    }
    public void AddCustomer(string ssn, string lastname, string firstname)
    {
        try
        {
            if (ssn.Length < 1 || lastname.Length < 1 || lastname.Length < 1)
                throw new ArgumentException(error);

            var collection = GetCustomers(ssn).ToList();

            if (collection.Count > 0) throw new ArgumentNullException(error);
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
        _in.Clear();
    }
    public async Task<IBooking?> RentVehicle(int vehicleId, int customerId)
    {
        IBooking? booking;
        IBooking? nobooking = default;
        error = string.Empty;

        try
        {
            customerId = Convert.ToInt32(_in.customer);
            if (vehicleId < 1 || customerId < 1) throw new ArgumentException(error);
            processing = true;
            await Task.Delay(5000);
            booking = await Task.Run(() => _data.RentVehicle(vehicleId, customerId));
            processing = false;
            _in.Clear();
            return booking ?? throw new Exception("Could not rent vehicle");
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

        if (vehicleId < 1) throw new ArgumentException("Possible null value");
        booking = _data.ReturnVehicle(vehicleId);

        try
        {
            double distance = Convert.ToDouble(ddistance);
            if (ddistance.Length < 0 || distance == 0) throw new ArgumentException(error);

            error = string.Empty;

            if (booking is not null)
            {
                booking.ReturnedKm = (distance + booking.Vehicle?.Odometer);
                booking.ReturnDate = DateTime.Now;
                booking.Status = VehicleStatus.Available;
                booking.ReturnVehicle(booking.Vehicle);
                if (booking.ReturnedKm is null || distance == 0) throw new ArgumentException(error);
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
            if (vehicle is null || distance == 0) throw new ArgumentException("Possible null value");
            error = string.Empty;

            if (vehicle is not null)
            {
                vehicle.Odometer += distance;
                vehicle.Status = VehicleStatus.Available;
            }
        }
        catch
        {
            throw;
        }
        return vehicle ?? throw new Exception("Could not update vehicle");
    }
}
