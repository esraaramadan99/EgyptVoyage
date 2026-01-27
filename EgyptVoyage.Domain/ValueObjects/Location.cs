using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Domain.ValueObjects;

/// <summary>
/// Represents a geographical location
/// </summary>
public class Location
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //public double? Latitude { get; set; }
    //public double? Longitude { get; set; }

    public Location() { }

    public Location(string city, string address) //double? latitude = null, double? longitude = null)
    {
        City = city;
        Address = address;
       // Latitude = latitude;
        //Longitude = longitude;
    }
}