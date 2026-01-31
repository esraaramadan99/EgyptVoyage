using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Infrastructure.Repositories;

/// <summary>
/// Landmark repository implementation
/// </summary>
public class LandmarkRepository : Repository<Landmark>, ILandmarkRepository
{
    public LandmarkRepository(MongoDbContext context) : base(context.Landmarks)
    {
    }

    public async Task<List<Landmark>> SearchByNameAsync(string name)
    {
        var filter = Builders<Landmark>.Filter.And(
            Builders<Landmark>.Filter.Regex(x => x.LandmarkName, new MongoDB.Bson.BsonRegularExpression(name, "i")),
            Builders<Landmark>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).ToListAsync();
    }
}