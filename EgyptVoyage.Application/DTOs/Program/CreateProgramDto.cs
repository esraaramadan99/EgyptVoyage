using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptVoyage.Application.DTOs.Program;
/*
public class CreateProgramDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
*/
public class CreateProgramDto
{
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Duration value (number of days or hours)
    /// </summary>
    public double DurationValue { get; set; }

    /// <summary>
    /// Unit of duration: "Days" or "Hours"
    /// </summary>
    public string DurationUnit { get; set; } = "Days";

    /// <summary>
    /// Calculated TimeSpan
    /// </summary>
    public TimeSpan Duration => DurationUnit.ToLower() switch
    {
        "hours" => TimeSpan.FromHours(DurationValue),
        _ => TimeSpan.FromDays(DurationValue)
    };

    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
