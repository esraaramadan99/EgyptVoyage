using EgyptVoyage.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Infrastructure.Data;

/// <summary>
/// MongoDB database context
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _settings;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;

        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    // Collections
    public IMongoCollection<Hotel> Hotels =>
        _database.GetCollection<Hotel>(_settings.HotelsCollectionName);

    public IMongoCollection<Restaurant> Restaurants =>
        _database.GetCollection<Restaurant>(_settings.RestaurantsCollectionName);

    public IMongoCollection<Landmark> Landmarks =>
        _database.GetCollection<Landmark>(_settings.LandmarksCollectionName);

    public IMongoCollection<Program> Programs =>
        _database.GetCollection<Program>(_settings.ProgramsCollectionName);

    public IMongoCollection<Tourist> Tourists =>
        _database.GetCollection<Tourist>(_settings.TouristsCollectionName);

    public IMongoCollection<Review> Reviews =>
        _database.GetCollection<Review>(_settings.ReviewsCollectionName);

    public IMongoCollection<FavoriteList> FavoriteLists =>
        _database.GetCollection<FavoriteList>(_settings.FavoriteListsCollectionName);
}