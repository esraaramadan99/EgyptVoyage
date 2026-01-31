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
/// Restaurant repository implementation
/// </summary>
public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(MongoDbContext context) : base(context.Restaurants)
    {
    }

    public async Task<List<Restaurant>> SearchByNameAsync(string name)
    {
        var filter = Builders<Restaurant>.Filter.And(
            Builders<Restaurant>.Filter.Regex(x => x.RestaurantName, new MongoDB.Bson.BsonRegularExpression(name, "i")),
            Builders<Restaurant>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).ToListAsync();
    }
}