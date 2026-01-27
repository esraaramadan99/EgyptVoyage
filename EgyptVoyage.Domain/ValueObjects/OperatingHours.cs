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
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

    public OperatingHours() { }

    public OperatingHours(TimeSpan openingTime, TimeSpan closingTime)
    {
        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }

    public bool IsOpen(TimeSpan currentTime)
    {
        return currentTime >= OpeningTime && currentTime <= ClosingTime;
    }
}