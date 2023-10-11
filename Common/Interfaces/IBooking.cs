using Common.Enums;

namespace Common.Interfaces;

public interface IBooking
{
    public string RegNo { get; init; }
    public IPerson Person { get; init; }
    public IVehicle Vehicle { get; init; }
    public int? ReturnedKm { get; set; }
    public int StartKm { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public double? Cost { get; set; }
    public VehicleStatus Status { get; set; }

}
