using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;

namespace Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _persons = new();
    readonly List<IVehicle> _vehicles = new();
    readonly List<IBooking> _bookings = new();
    public CollectionData() => SeedData();
    void SeedData()
    {

        var rentaldate1 = new DateTime(2023, 09, 15);
        var rentaldate2 = new DateTime(2023, 09, 27);
        var rentaldate3 = new DateTime(2023, 10, 02);

        var returndate2 = DateTime.Today;
        var returndate3 = new DateTime(2023, 10, 04);

        var returnedKm2 = 20300;
        var returnedKm3 = 20500;

        var person1 = new Person(45123, "Elgh", "Moa");
        var person2 = new Person(45663, "Fager", "Nils");
        var person3 = new Person(88563, "Höve", "Anna");
        _persons.Add(person1);
        _persons.Add(person2);
        _persons.Add(person3);

        var vehicle1 = new Car("JUH458", "Volvo", 10000, 1, VehicleType.Combi, 200, VehicleStatus.Booked);
        var vehicle2 = new Car("MAN985", "Saab", 20000, 1, VehicleType.Sedan, 100, VehicleStatus.Available);
        var vehicle3 = new Motorcycle("NUJ741", "Yamaha", 20000, 0.5, VehicleType.Motorcycle, 50, VehicleStatus.Available);
        _vehicles.Add(vehicle1);
        _vehicles.Add(vehicle2);
        _vehicles.Add(vehicle3);

        _vehicles.Add(new Car("WEK864", "Tesla", 1000, 3, VehicleType.Sedan, 100, VehicleStatus.Available));
        _vehicles.Add(new Car("KYT185", "Jeep", 5000, 1.5, VehicleType.Van, 300, VehicleStatus.Available));

        var booking1 = new Booking(person1, vehicle1, rentaldate1);
        var booking2 = new Booking(person2, vehicle2, returnedKm2, rentaldate2, returndate2);
        var booking3 = new Booking(person3, vehicle3, returnedKm3, rentaldate3, returndate3);

        booking2.ReturnVehicle(vehicle2);
        booking3.ReturnVehicle(vehicle3);

        _bookings.Add(booking1);
        _bookings.Add(booking2);
        _bookings.Add(booking3);

    }

    public IEnumerable<IPerson> GetPersons() => _persons;
    public IEnumerable<IVehicle> GetVehicles(VehicleStatus status = default) => _vehicles;
    public IEnumerable<IBooking> GetBookings() => _bookings;
}
