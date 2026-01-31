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
/// Clerk repository implementation
/// </summary>
public class ClerkRepository : Repository<Clerk>, IClerkRepository
{
    public ClerkRepository(MongoDbContext context) : base(context.Clerks)
    {
    }

    public async Task<Clerk?> GetByEmailAsync(string email)
    {
        var filter = Builders<Clerk>.Filter.And(
            Builders<Clerk>.Filter.Eq(x => x.Email, email),
            Builders<Clerk>.Filter.Eq(x => x.IsDeleted, false)
        );
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var filter = Builders<Clerk>.Filter.And(
            Builders<Clerk>.Filter.Eq(x => x.Email, email),
            Builders<Clerk>.Filter.Eq(x => x.IsDeleted, false)
        );
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }
}