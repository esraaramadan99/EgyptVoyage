using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Domain.ValueObjects;

/// <summary>
/// Represents operating hours for restaurants and landmarks
/// </summary>
public class OperatingHours
{
    public TimeSpan OpeningHour { get; set; }
    public TimeSpan ClosingHour { get; set; }

    public OperatingHours() { }

    public OperatingHours(TimeSpan openingHour, TimeSpan closingHour)
    {
        OpeningHour = openingHour;
        ClosingHour = closingHour;
    }

    public bool IsOpen(TimeSpan currentTime)
    {
        return currentTime >= OpeningHour && currentTime <= ClosingHour;
    }
}