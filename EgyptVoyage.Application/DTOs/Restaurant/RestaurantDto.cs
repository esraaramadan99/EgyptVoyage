using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Restaurant;

public class RestaurantDto
{
    public string Id { get; set; } = string.Empty;
    public string RestaurantName { get; set; } = string.Empty;
    public string OpeningHour { get; set; } = string.Empty;
    public string CloseHour { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public string CuisineType { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public string Menu { get; set; } = string.Empty;
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