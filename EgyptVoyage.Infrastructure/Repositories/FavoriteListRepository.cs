using AutoMapper;
using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Application.DTOs.Program;
using EgyptVoyage.Application.DTOs.Restaurant;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using MongoDB.Driver;

namespace EgyptVoyage.Infrastructure.Repositories;

public class FavoriteListRepository : Repository<FavoriteList>, IFavoriteListRepository
{
    // بنعلن عن متغيرين private readonly
    // private = متاح في الـ class دي بس
    // readonly = بيتحدد مرة واحدة في الـ constructor ومش بيتغير
    private readonly MongoDbContext _context;
    private readonly IMapper _mapper;

    public FavoriteListRepository(MongoDbContext context, IMapper mapper)
        : base(context.FavoriteLists)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FavoriteList?> GetByTouristIdAsync(string touristId)
    {
        var filter = Builders<FavoriteList>.Filter.And(
            Builders<FavoriteList>.Filter.Eq(x => x.TouristId, touristId),
            Builders<FavoriteList>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<FavoriteDetailDto> GetByTouristIdWithDetailsAsync(string touristId)
    {
        var favorite = await GetByTouristIdAsync(touristId);
        if (favorite == null)
            return new FavoriteDetailDto { TouristId = touristId };
        return await BuildDetailDto(favorite);
    }

    public async Task<string> GenerateShareTokenAsync(string touristId)
    {
        var favorite = await GetByTouristIdAsync(touristId);
        if (favorite == null)
        {
            favorite = new FavoriteList { TouristId = touristId };
            await AddAsync(favorite);
        }

        var shareToken = Guid.NewGuid().ToString("N");

        var filter = Builders<FavoriteList>.Filter.Eq(x => x.Id, favorite.Id);
        var update = Builders<FavoriteList>.Update
            .Set(x => x.ShareToken, shareToken)
            .Set(x => x.IsPublic, true)
            .Set(x => x.SharedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update);
        return shareToken;
    }

    public async Task<FavoriteDetailDto?> GetByShareTokenAsync(string shareToken)
    {
        var filter = Builders<FavoriteList>.Filter.And(
            Builders<FavoriteList>.Filter.Eq(x => x.ShareToken, shareToken),
            Builders<FavoriteList>.Filter.Eq(x => x.IsPublic, true),
            Builders<FavoriteList>.Filter.Eq(x => x.IsDeleted, false)
        );
        var favorite = await _collection.Find(filter).FirstOrDefaultAsync();
        if (favorite == null) return null;
        return await BuildDetailDto(favorite);
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

    private async Task<FavoriteDetailDto> BuildDetailDto(FavoriteList favorite)
    {
        var result = new FavoriteDetailDto
        {
            Id = favorite.Id,
            TouristId = favorite.TouristId
        };

        if (favorite.HotelIds.Any())
        {
            var hotels = await _context.Hotels
                .Find(Builders<Hotel>.Filter.And(
                    Builders<Hotel>.Filter.In(x => x.Id, favorite.HotelIds),
                    Builders<Hotel>.Filter.Eq(x => x.IsDeleted, false)))
                .ToListAsync();
            result.Hotels = _mapper.Map<List<HotelDto>>(hotels);
        }

        if (favorite.RestaurantIds.Any())
        {
            var restaurants = await _context.Restaurants
                .Find(Builders<Restaurant>.Filter.And(
                    Builders<Restaurant>.Filter.In(x => x.Id, favorite.RestaurantIds),
                    Builders<Restaurant>.Filter.Eq(x => x.IsDeleted, false)))
                .ToListAsync();
            result.Restaurants = _mapper.Map<List<RestaurantDto>>(restaurants);
        }

        if (favorite.LandmarkIds.Any())
        {
            var landmarks = await _context.Landmarks
                .Find(Builders<Landmark>.Filter.And(
                    Builders<Landmark>.Filter.In(x => x.Id, favorite.LandmarkIds),
                    Builders<Landmark>.Filter.Eq(x => x.IsDeleted, false)))
                .ToListAsync();
            result.Landmarks = _mapper.Map<List<LandmarkDto>>(landmarks);
        }

        if (favorite.ProgramIds.Any())
        {
            var programs = await _context.Programs
                .Find(Builders<Program>.Filter.And(
                    Builders<Program>.Filter.In(x => x.Id, favorite.ProgramIds),
                    Builders<Program>.Filter.Eq(x => x.IsDeleted, false)))
                .ToListAsync();
            result.Programs = _mapper.Map<List<ProgramDto>>(programs);
        }

        return result;
    }
}