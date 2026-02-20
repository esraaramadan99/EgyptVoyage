using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;
using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Domain.Entities;


public class Restaurant : BaseEntity
{
    public string RestaurantName { get; set; } = string.Empty;

    public TimeSpan OpeningHour { get; set; }
    public TimeSpan ClosingHour { get; set; }
    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public CuisineType CuisineType { get; set; }
    public Location Location { get; set; } = null!;
   // public string Menu { get; set; } = string.Empty;
    public double Rating { get; set; }
}
















