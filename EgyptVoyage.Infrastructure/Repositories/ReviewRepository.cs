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
/// Review repository implementation
/// </summary>
public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(MongoDbContext context) : base(context.Reviews)
    {
    }

    public async Task<List<Review>> GetByEntityAsync(string entityId)
    {
        var filter = Builders<Review>.Filter.And(
            Builders<Review>.Filter.Eq(x => x.EntityId, entityId),
            Builders<Review>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Review>> GetByTouristAsync(string touristId)
    {
        var filter = Builders<Review>.Filter.And(
            Builders<Review>.Filter.Eq(x => x.TouristId, touristId),
            Builders<Review>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}