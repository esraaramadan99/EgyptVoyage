using EgyptVoyage.Domain.Entities.Common;

namespace EgyptVoyage.Domain.Entities;

public class FavoriteList : BaseEntity
{
    public string TouristId { get; set; } = string.Empty;
    public List<string> HotelIds { get; set; } = new();
    public List<string> RestaurantIds { get; set; } = new();
    public List<string> LandmarkIds { get; set; } = new();
    public List<string> ProgramIds { get; set; } = new();
    public string? ShareToken { get; set; }
    public bool IsPublic { get; set; } = false;
    public DateTime? SharedAt { get; set; }
}

