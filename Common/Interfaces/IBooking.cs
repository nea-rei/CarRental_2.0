using Common.Classes;
using Common.Enums;

namespace Common.Interfaces;

public interface IBooking
{
    public int Id { get; }
    public string RegNo { get; init; }
    public IPerson Person { get; init; }
    public IVehicle Vehicle { get; init; }
    public double StartKm { get; set; }
    public double? ReturnedKm { get; set; }
    public DateTime RentalDate { get; init; }
    public DateTime? ReturnDate { get; set; }
    public double? Cost { get; }
    public VehicleStatus Status { get; set; }

    void AssignId(int id);
    void ReturnVehicle(IVehicle vehicle);

}
