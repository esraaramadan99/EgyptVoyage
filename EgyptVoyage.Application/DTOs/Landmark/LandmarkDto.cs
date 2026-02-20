using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptVoyage.Application.DTOs.Landmark;

public class LandmarkDto
{
    public string Id { get; set; } = string.Empty;
    public string LandmarkName { get; set; } = string.Empty;
    //public OperatingHours OperatingHours { get; set; } = null!;

    public string OpeningHour { get; set; } = string.Empty;
    public string ClosingHour { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;

    public double Price { get; set; }
    public double Rating { get; set; }

}

/*public class LocationDto
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //public double? Latitude { get; set; }
    //public double? Longitude { get; set; }
}
*/