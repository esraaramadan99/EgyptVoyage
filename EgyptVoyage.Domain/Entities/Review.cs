using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;

namespace EgyptVoyage.Domain.Entities;

/// <summary>
/// Review entity
/// </summary>
public class Review : BaseEntity
{
    public string TouristId { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public FavoriteType EntityType { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}