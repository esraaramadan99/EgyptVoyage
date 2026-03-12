using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Infrastructure.Authentication;
using EgyptVoyage.Infrastructure.Data;
using EgyptVoyage.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EgyptVoyage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<MongoDbContext>();

        // Repositories الموجودة
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<ILandmarkRepository, LandmarkRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ITouristRepository, TouristRepository>();
        services.AddScoped<IFavoriteListRepository, FavoriteListRepository>();

        // Repository الجديد للـ Clerk
        services.AddScoped<IClerkRepository, ClerkRepository>();

        services.AddSingleton<JwtTokenGenerator>();
        services.AddSingleton<PasswordHasher>();

        return services;
    }
}