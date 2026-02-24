using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using MongoDB.Driver;

namespace EgyptVoyage.Infrastructure.Repositories;

public class FavoriteListRepository : Repository<FavoriteList>, IFavoriteListRepository
{
    public FavoriteListRepository(MongoDbContext context) : base(context.FavoriteLists) { }

    public async Task<FavoriteList?> GetByTouristIdAsync(string touristId)
    {
        var filter = Builders<FavoriteList>.Filter.And(
            Builders<FavoriteList>.Filter.Eq(x => x.TouristId, touristId),
            Builders<FavoriteList>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<FavoriteList> AddItemAsync(string touristId, string entityType, string entityId)
    {
        var favorite = await GetByTouristIdAsync(touristId);
        if (favorite == null)
        {
            favorite = new FavoriteList { TouristId = touristId };
            await AddAsync(favorite);
        }

        UpdateDefinition<FavoriteList> update = entityType.ToLower() switch
        {
            "hotel" => Builders<FavoriteList>.Update.AddToSet(x => x.HotelIds, entityId),
            "restaurant" => Builders<FavoriteList>.Update.AddToSet(x => x.RestaurantIds, entityId),
            "landmark" => Builders<FavoriteList>.Update.AddToSet(x => x.LandmarkIds, entityId),
            "program" => Builders<FavoriteList>.Update.AddToSet(x => x.ProgramIds, entityId),
            _ => throw new ArgumentException($"Invalid entity type: {entityType}")
        };

        var filter = Builders<FavoriteList>.Filter.Eq(x => x.Id, favorite.Id);
        await _collection.UpdateOneAsync(filter, update);

        return (await GetByTouristIdAsync(touristId))!;
    }

    public async Task<bool> RemoveItemAsync(string touristId, string entityType, string entityId)
    {
        var favorite = await GetByTouristIdAsync(touristId);
        if (favorite == null) return false;

        UpdateDefinition<FavoriteList> update = entityType.ToLower() switch
        {
            "hotel" => Builders<FavoriteList>.Update.Pull(x => x.HotelIds, entityId),
            "restaurant" => Builders<FavoriteList>.Update.Pull(x => x.RestaurantIds, entityId),
            "landmark" => Builders<FavoriteList>.Update.Pull(x => x.LandmarkIds, entityId),
            "program" => Builders<FavoriteList>.Update.Pull(x => x.ProgramIds, entityId),
            _ => throw new ArgumentException($"Invalid entity type: {entityType}")
        };

        var filter = Builders<FavoriteList>.Filter.Eq(x => x.Id, favorite.Id);
        var result = await _collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }
}