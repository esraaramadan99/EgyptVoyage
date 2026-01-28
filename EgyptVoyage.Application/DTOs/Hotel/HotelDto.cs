using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Hotel;

public class HotelDto
{
    public string Id { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string WebsiteLink { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public List<string> Images { get; set; } = new();
}

/*public class LocationDto
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
   // public double? Latitude { get; set; }
    //public double? Longitude { get; set; }
}
*/