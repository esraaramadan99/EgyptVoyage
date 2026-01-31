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
/// Hotel repository implementation
/// </summary>
public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(MongoDbContext context) : base(context.Hotels)
    {
    }

    public async Task<List<Hotel>> SearchByNameAsync(string name)
    {
        var filter = Builders<Hotel>.Filter.And(
            Builders<Hotel>.Filter.Regex(x => x.HotelName, new MongoDB.Bson.BsonRegularExpression(name, "i")),
            Builders<Hotel>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).ToListAsync();
    }
}