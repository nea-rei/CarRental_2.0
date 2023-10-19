using Common.Interfaces;

namespace Common.Extensions;

public static class BookingExtensions
{
    public static double Duration(this DateTime returndate, DateTime rentaldate)
    {
        TimeSpan span = returndate.Subtract(rentaldate);
        var days = span.Days;
        return days;
    }
}
