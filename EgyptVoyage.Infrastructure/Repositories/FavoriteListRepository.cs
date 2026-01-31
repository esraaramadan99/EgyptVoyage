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
/// FavoriteList repository implementation
/// </summary>
public class FavoriteListRepository : Repository<FavoriteList>, IFavoriteListRepository
{
    public FavoriteListRepository(MongoDbContext context) : base(context.FavoriteLists)
    {
    }

    public async Task<FavoriteList?> GetByTouristIdAsync(string touristId)
    {
        var filter = Builders<FavoriteList>.Filter.And(
            Builders<FavoriteList>.Filter.Eq(x => x.TouristId, touristId),
            Builders<FavoriteList>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}