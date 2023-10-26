using Common.Enums;
using Common.Interfaces;
using Common.Extensions;

namespace Common.Classes;

public class Booking : IBooking
{
    public int Id { get; private set; }
    public string RegNo { get; } = string.Empty;
    public IPerson Person { get; }
    public IVehicle Vehicle { get; }
    public double StartKm { get; set; }
    public double? ReturnedKm { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public VehicleStatus Status { get; set; }
    public double? Cost { get; set; }

    public void AssignId(int id) => Id = id;
    public Booking(IPerson person, IVehicle vehicle, DateTime rentaldate)
    {

        RegNo = vehicle.RegNo;
        Person = person;
        Vehicle = vehicle;
        StartKm = vehicle.Odometer;
        RentalDate = rentaldate;
        Status = vehicle.Status;
    }

    public Booking(int id, IPerson person, IVehicle vehicle, double startkm, double returnedkm, DateTime rentaldate, DateTime returndate)
    {
        Id = id;
        RegNo = vehicle.RegNo;
        Person = person;
        Vehicle = vehicle;
        RentalDate = rentaldate;
        ReturnDate = returndate;
        StartKm = startkm;
        ReturnedKm = returnedkm ;
        Status = vehicle.Status;
    }
    public void ReturnVehicle(IVehicle vehicle)
    {

        Cost = (ReturnDate?.Duration(RentalDate) * vehicle.DailyCost) + ((ReturnedKm - vehicle.Odometer) * vehicle.CostKm);
    }

}
