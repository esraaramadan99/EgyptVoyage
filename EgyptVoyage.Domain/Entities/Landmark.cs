using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;
using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Domain.Entities;

public class Landmark : BaseEntity
{
    public string LandmarkName { get; set; } = string.Empty;
    public string ImageCover { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Location Location { get; set; } = null!;
    public TimeSpan OpeningHour { get; set; }
    public TimeSpan ClosingHour { get; set; }
    public List<string> Images { get; set; } = new();
    public double Price { get; set; }
    public double Rating { get; set; }

   // public List<string> Videos { get; set; } = new();
}