using Common.Enums;
using Common.Interfaces;
using Common.Extensions;

namespace Common.Classes;

public class Booking : IBooking
{
    public string RegNo { get; init; } = string.Empty;
    public IPerson Person { get; init; }
    public IVehicle Vehicle { get; init; }
    public int StartKm { get; set; }
    public int? ReturnedKm { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public VehicleStatus Status { get; set; }
    public double? Cost { get; set; }

    public Booking(IPerson person, IVehicle vehicle, DateTime rentaldate)
    {
        RegNo = vehicle.RegNo;
        Person = person;
        Vehicle = vehicle;
        StartKm = vehicle.Odometer;
        RentalDate = rentaldate;
        Status = vehicle.Status;
    }
    public Booking(IPerson person, IVehicle vehicle, int returnedkm, DateTime rentaldate, DateTime returndate)
    {
        RegNo = vehicle.RegNo;
        Person = person;
        Vehicle = vehicle;
        RentalDate = rentaldate;
        ReturnDate = returndate;
        StartKm = vehicle.Odometer;
        ReturnedKm = returnedkm;
        Status = vehicle.Status;
    }
    public void ReturnVehicle(IVehicle vehicle)
    {

        Cost = (ReturnDate?.Duration(RentalDate) * vehicle.DailyCost) + ((ReturnedKm - vehicle.Odometer) * vehicle.CostKm);
    }
}
