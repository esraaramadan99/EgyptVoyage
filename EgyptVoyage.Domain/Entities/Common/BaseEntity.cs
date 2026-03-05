using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EgyptVoyage.Domain.Entities.Common;

public abstract class BaseEntity
{
    // variable mn mongo by2ol en de hya el primary key

    //BSON = Binary JSON — الصيغة اللي MongoDB بتخزن فيها البيانات فعليا
    [BsonId]
    // for converting (ID) as a objectid => c# as a string

    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    // time for creating a document 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //   ؟ ممكن تبقي null عادي
    public DateTime? UpdatedAt { get; set; }

    // soft delete pattern instead of deleting records from db
    public bool IsDeleted { get; set; } = false;
}
