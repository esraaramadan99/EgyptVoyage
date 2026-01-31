using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for MongoDB
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> _collection;

    public Repository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq(x => x.Id, id),
            Builders<T>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        var filter = Builders<T>.Filter.Eq(x => x.IsDeleted, false);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsDeleted = false;
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);

        return entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        // Soft delete
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        var update = Builders<T>.Update
            .Set(x => x.IsDeleted, true)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq(x => x.Id, id),
            Builders<T>.Filter.Eq(x => x.IsDeleted, false)
        );
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }

    public async Task<long> CountAsync()
    {
        var filter = Builders<T>.Filter.Eq(x => x.IsDeleted, false);
        return await _collection.CountDocumentsAsync(filter);
    }
}