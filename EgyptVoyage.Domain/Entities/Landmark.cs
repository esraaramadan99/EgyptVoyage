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
/// Landmark entity
/// </summary>
public class Landmark : BaseEntity
{
    public string LandmarkName { get; set; } = string.Empty;
    public OperatingHours? OperatingHours { get; set; }
    public List<string> Images { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public Location Location { get; set; } = null!;
    public LandmarkType Type { get; set; }
}