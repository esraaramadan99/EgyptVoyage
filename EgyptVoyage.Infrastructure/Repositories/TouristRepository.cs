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
/// Tourist repository implementation
/// </summary>
public class TouristRepository : Repository<Tourist>, ITouristRepository
{
    public TouristRepository(MongoDbContext context) : base(context.Tourists)
    {
    }

    public async Task<Tourist?> GetByEmailAsync(string email)
    {
        var filter = Builders<Tourist>.Filter.And(
            Builders<Tourist>.Filter.Eq(x => x.Email, email),
            Builders<Tourist>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var filter = Builders<Tourist>.Filter.And(
            Builders<Tourist>.Filter.Eq(x => x.Email, email),
            Builders<Tourist>.Filter.Eq(x => x.IsDeleted, false)
        );
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }
}