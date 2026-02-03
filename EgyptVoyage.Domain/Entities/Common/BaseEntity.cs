using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EgyptVoyage.Domain.Entities.Common;

/// <summary>
/// Base entity for all domain entities
/// </summary>
public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
