using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Program;
/*
public class ProgramDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
*/
/*public class LocationDto
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //public double? Latitude { get; set; }
    //public double? Longitude { get; set; }
}
*/
public class ProgramDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Duration of the program in days
    /// </summary>
    public string Duration { get; set; } = string.Empty;

    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();

    public string ImageCover { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
