using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using MongoDB.Driver;

namespace EgyptVoyage.Infrastructure.Repositories;

public class ClerkRepository : Repository<Clerk>, IClerkRepository
{
    public ClerkRepository(MongoDbContext context) : base(context.Clerks)
    {
    }

    public async Task<Clerk?> GetByEmailAsync(string email)
    {
        // بنجيب كل الـ Clerks الموجودين مش Deleted
        var filter = Builders<Clerk>.Filter.Eq(x => x.IsDeleted, false);
        var allClerks = await _collection.Find(filter).ToListAsync();

        // Debug: بنعمل مقارنة يدوية
        return allClerks.FirstOrDefault(x =>
            x.Email.Trim().ToLower() == email.Trim().ToLower());
    }
}
