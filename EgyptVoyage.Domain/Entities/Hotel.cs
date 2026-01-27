using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EgyptVoyage.Domain.Enums;


namespace EgyptVoyage.Domain.Entities;

/// <summary>
/// Hotel entity
/// </summary>
public class Hotel : BaseEntity
{
    public string HotelName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string WebsiteLink { get; set; } = string.Empty;
    public HotelLevel Level { get; set; }
    public string Description { get; set; } = string.Empty;
    public Location Location { get; set; } = null!;
    public List<string> Images { get; set; } = new();
}
