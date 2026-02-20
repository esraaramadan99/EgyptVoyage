using EgyptVoyage.Domain.ValueObjects;
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

    //public OperatingHours OperatingHours { get; set; } = null!;

    public string OpeningHour { get; set; } = string.Empty;
    public string ClosingHour { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public double Rating { get; set; }
}