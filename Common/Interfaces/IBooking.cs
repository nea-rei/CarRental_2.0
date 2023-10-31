using Common.Classes;
using Common.Enums;

namespace Common.Interfaces;

public interface IBooking
{
    public int Id { get; }
    public string RegNo { get; set; }
    public IPerson? Person { get; set; }
    public IVehicle? Vehicle { get; set; }
    public double StartKm { get; set; }
    public double? ReturnedKm { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public double? Cost { get; }
    public VehicleStatus Status { get; set; }

    void AssignId(int id);
    void ReturnVehicle(IVehicle? vehicle);

}
