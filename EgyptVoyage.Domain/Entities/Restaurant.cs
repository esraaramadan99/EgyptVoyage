using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;
using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Domain.Entities;

/// <summary>
/// Restaurant entity
/// </summary>
/// 
/*
public class Restaurant : BaseEntity
{
    public string RestaurantName { get; set; } = string.Empty;
    public OperatingHours? OperatingHours { get; set; }
    public List<string> Images { get; set; } = new();
    public CuisineType CuisineType { get; set; }
    public Location Location { get; set; } = null!;
    public string Menu { get; set; } = string.Empty;
    public double Rating { get; set; }
}

*/
public class Restaurant : BaseEntity
{
    public string RestaurantName { get; set; } = string.Empty;
    public OperatingHours? OperatingHours { get; set; }
    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public CuisineType CuisineType { get; set; }
    public Location Location { get; set; } = null!;
   // public string Menu { get; set; } = string.Empty;
    public double Rating { get; set; }
}
















