using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Domain.Entities;

/// <summary>
/// Tourism program entity
/// </summary>
/// 
/*
public class Program : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public Location Location { get; set; } = null!;

    public string Image { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
*/
public class Program : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Duration of the program in days
    /// </summary>
    public required string Duration { get; set; }

    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}