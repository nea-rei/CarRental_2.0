using Common.Classes;
using Common.Enums;
using Common.Interfaces;
using Data.Interfaces;
using Common.Exceptions;
using System.Reflection;

namespace Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _persons = new();
    readonly List<IVehicle> _vehicles = new();
    readonly List<IBooking> _bookings = new();
    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(x => x.Id) +1;
    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(p => p.Id) +1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatus));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleType));

    public VehicleType GetVehicleType(string name)
    {
        try
        {
            return (VehicleType)Enum.Parse(typeof(VehicleType), name);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public int GetDailyCost(VehicleType type)
    {
        try
        {
            int dailycost = 0;
            switch (type)
            {
                case VehicleType.Sedan:
                    dailycost = 100;
                    break;
                case VehicleType.Van:
                    dailycost = 300;
                    break;
                case VehicleType.Combi:
                    dailycost = 200;
                    break;
                case VehicleType.Motorcycle:
                    dailycost = 50;
                    break;
            };
            return dailycost;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public List<T> Get<T>(Func<T, bool>? lambda = null)
    {
        try
        {
            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

            var value = collections.GetValue(this) ?? throw new InvalidDataException();

            var collection = ((List<T>)value).AsQueryable();

            if (lambda is null) return collection.ToList();

            return collection.Where(lambda).ToList();
        }
        catch (Exception)
        {

            throw;
        }

    }
    public T? Single<T>(Func<T, bool>? lambda)
    {
        try
        {
            if (lambda is null) throw new DataNullException("could not find lambda expression");

            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

            var value = collections.GetValue(this) ?? throw new InvalidDataException();

            var collection = ((List<T>)value).AsQueryable();

            var item = collection.SingleOrDefault(lambda);

            return item ?? throw new InvalidOperationException("More than one matching item found.");
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void Add<T>(T item)
    {
        try
        {
            if (item is null) throw new DataNullException("Could not add item");

            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

            var value = collections.GetValue(this) ?? throw new InvalidDataException();

            ((List<T>)value).Add(item);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public IBooking RentVehicle(int vehicleId, int customerId)
    {
        try
        {
            Random rdm = new();

            var person = Single<IPerson>(p => p.Id == customerId);
            var vehicle = Single<IVehicle>(v => v.Id == vehicleId);
            var rentaldate = DateTime.Now.AddDays(-(rdm.Next(1, 9)));


            if (person is null || vehicle is null) throw new DataNullException("person or vehicle does not exist");

            vehicle.UpdateStatus(VehicleStatus.Booked);

            var booking = new Booking(person, vehicle, rentaldate);

            var id = NextBookingId;
            booking.AssignId(id);

            if (booking is null) throw new DataNullException("booking does not exist");
                    Add<IBooking>(booking);

            return booking ?? throw new Exception("could not create booking");
        }
        catch (Exception)
        {

            throw;
        }
    }
    public IBooking ReturnVehicle(int vehicleId)
    {
        try
        {
            var booking = Single<IBooking>(v => v.Vehicle.Id == vehicleId && v.Cost.Equals(null));

            return booking ?? throw new Exception("could not find booking");
        }
        catch (Exception)
        {

            throw;
        }
    }
    public CollectionData() => SeedData();
    void SeedData()
    {
        _persons.Add(new Customer(1, "45123", "Elgh", "Moa"));
        _persons.Add(new Customer(2, "45663", "Fager", "Nils"));
        _persons.Add(new Customer(3, "43463", "Andersson", "Hanna"));

        _vehicles.Add(new Car(2, "WEK864", "Tesla", 1000, 3, 100, VehicleType.Sedan, VehicleStatus.Available));
        _vehicles.Add(new Car(1, "KYT185", "Jeep", 15000, 1.5, 300, VehicleType.Van, VehicleStatus.Available));
        _vehicles.Add(new Motorcycle(3, "REM321", "Yamaha", 3000, 0.5, 50, VehicleType.Motorcycle, VehicleStatus.Available));

    }
}
