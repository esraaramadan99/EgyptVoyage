
/*
using AutoMapper;
using EgyptVoyage.Application.DTOs;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Restaurant;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Application.DTOs.Program;
using EgyptVoyage.Application.DTOs.Review;
using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Domain.ValueObjects;
using EgyptVoyage.Domain.Enums;

namespace EgyptVoyage.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile with all entity-DTO mappings
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ============================================
        // Location Mappings
        // ============================================
        CreateMap<Location, LocationDto>().ReverseMap();

        // ============================================
        // Auth Mappings
        // ============================================
        CreateMap<RegisterDto, Tourist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // ============================================
        // Hotel Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Level));

        // DTO -> Entity (Create)
        CreateMap<CreateHotelDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));

        // DTO -> Entity (Update)
        CreateMap<UpdateHotelDto, Hotel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));

        // ============================================
        // Restaurant Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom<RestaurantOpeningHourResolver>())
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom<RestaurantCloseHourResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom(src => src.CuisineType.ToString()));

        // DTO -> Entity (Create)
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<CreateRestaurantOperatingHoursResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        // DTO -> Entity (Update)
        CreateMap<UpdateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<UpdateRestaurantOperatingHoursResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        // ============================================
        // Landmark Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<Landmark, LandmarkDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom<LandmarkOpeningHourResolver>())
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom<LandmarkCloseHourResolver>());

        // DTO -> Entity (Create)
        CreateMap<CreateLandmarkDto, Landmark>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<CreateLandmarkOperatingHoursResolver>());

        // DTO -> Entity (Update)
        CreateMap<UpdateLandmarkDto, Landmark>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<UpdateLandmarkOperatingHoursResolver>());

        // ============================================
        // Program Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<Domain.Entities.Program, ProgramDto>();

        // DTO -> Entity (Create)
        CreateMap<CreateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // DTO -> Entity (Update)
        CreateMap<UpdateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // ============================================
        // Review Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.EntityType, opt => opt.MapFrom(src => src.EntityType.ToString()));

        // DTO -> Entity (Create)
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TouristId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.EntityType, opt => opt.MapFrom<EntityTypeResolver>());

        // ============================================
        // FavoriteList Mappings
        // ============================================

        // Entity -> DTO (Read)
        CreateMap<FavoriteList, FavoriteDto>();

        // DTO -> Entity (Update)
        CreateMap<FavoriteDto, FavoriteList>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }

    // ============================================
    // Value Resolvers
    // ============================================

    // Restaurant Resolvers
    public class RestaurantOpeningHourResolver : IValueResolver<Restaurant, RestaurantDto, string>
    {
        public string Resolve(Restaurant source, RestaurantDto destination, string destMember, ResolutionContext context)
        {
            return source.OperatingHours?.OpeningTime.ToString(@"hh\:mm") ?? string.Empty;
        }
    }

    public class RestaurantCloseHourResolver : IValueResolver<Restaurant, RestaurantDto, string>
    {
        public string Resolve(Restaurant source, RestaurantDto destination, string destMember, ResolutionContext context)
        {
            return source.OperatingHours?.ClosingTime.ToString(@"hh\:mm") ?? string.Empty;
        }
    }

    public class CreateRestaurantOperatingHoursResolver : IValueResolver<CreateRestaurantDto, Restaurant, OperatingHours?>
    {
        public OperatingHours? Resolve(CreateRestaurantDto source, Restaurant destination, OperatingHours? destMember, ResolutionContext context)
        {
            return MapOperatingHours(source.OpeningHour, source.ClosingHour);
        }
    }

    public class UpdateRestaurantOperatingHoursResolver : IValueResolver<UpdateRestaurantDto, Restaurant, OperatingHours?>
    {
        public OperatingHours? Resolve(UpdateRestaurantDto source, Restaurant destination, OperatingHours? destMember, ResolutionContext context)
        {
            return MapOperatingHours(source.OpeningHour, source.ClosingHour);
        }
    }

    public class CuisineTypeResolver : IValueResolver<CreateRestaurantDto, Restaurant, CuisineType>,
                                       IValueResolver<UpdateRestaurantDto, Restaurant, CuisineType>
    {
        public CuisineType Resolve(CreateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
        {
            return ParseCuisineType(source.CuisineType);
        }

        public CuisineType Resolve(UpdateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
        {
            return ParseCuisineType(source.CuisineType);
        }
    }

    // Landmark Resolvers
    public class LandmarkOpeningHourResolver : IValueResolver<Landmark, LandmarkDto, string>
    {
        public string Resolve(Landmark source, LandmarkDto destination, string destMember, ResolutionContext context)
        {
            return source.OperatingHours?.OpeningTime.ToString(@"hh\:mm") ?? string.Empty;
        }
    }

    public class LandmarkCloseHourResolver : IValueResolver<Landmark, LandmarkDto, string>
    {
        public string Resolve(Landmark source, LandmarkDto destination, string destMember, ResolutionContext context)
        {
            return source.OperatingHours?.ClosingTime.ToString(@"hh\:mm") ?? string.Empty;
        }
    }

    public class CreateLandmarkOperatingHoursResolver : IValueResolver<CreateLandmarkDto, Landmark, OperatingHours?>
    {
        public OperatingHours? Resolve(CreateLandmarkDto source, Landmark destination, OperatingHours? destMember, ResolutionContext context)
        {
            return MapOperatingHours(source.OpeningHour, source.ClosingHour);
        }
    }

    public class UpdateLandmarkOperatingHoursResolver : IValueResolver<UpdateLandmarkDto, Landmark, OperatingHours?>
    {
        public OperatingHours? Resolve(UpdateLandmarkDto source, Landmark destination, OperatingHours? destMember, ResolutionContext context)
        {
            return MapOperatingHours(source.OpeningHour, source.ClosingHour);
        }
    }

    // Review Resolver
    public class EntityTypeResolver : IValueResolver<CreateReviewDto, Review, FavoriteType>
    {
        public FavoriteType Resolve(CreateReviewDto source, Review destination, FavoriteType destMember, ResolutionContext context)
        {
            return ParseFavoriteType(source.EntityType);
        }
    }

    // ============================================
    // Helper Methods
    // ============================================

    private static OperatingHours? MapOperatingHours(string openingHour, string closeHour)
    {
        if (string.IsNullOrWhiteSpace(openingHour) || string.IsNullOrWhiteSpace(closeHour))
            return null;

        try
        {
            var opening = TimeSpan.Parse(openingHour);
            var closing = TimeSpan.Parse(closeHour);
            return new OperatingHours(opening, closing);
        }
        catch
        {
            return null;
        }
    }

    private static CuisineType ParseCuisineType(string cuisineType)
    {
        if (Enum.TryParse<CuisineType>(cuisineType, true, out var result))
            return result;
        return CuisineType.Other;
    }

    private static FavoriteType ParseFavoriteType(string entityType)
    {
        if (Enum.TryParse<FavoriteType>(entityType, true, out var result))
            return result;
        return FavoriteType.Hotel;
    }
}
*/
using AutoMapper;
using EgyptVoyage.Application.DTOs;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Restaurant;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Application.DTOs.Program;
using EgyptVoyage.Application.DTOs.Review;
using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Domain.ValueObjects;
using EgyptVoyage.Domain.Enums;

namespace EgyptVoyage.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ============================================
        // Location
        // ============================================
        CreateMap<Location, LocationDto>().ReverseMap();

        // ============================================
        // Auth
        // ============================================
        CreateMap<RegisterDto, Tourist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // ============================================
        // Hotel — Email removed, ImageCover added
        // ============================================
        CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Level));

        CreateMap<CreateHotelDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));

        CreateMap<UpdateHotelDto, Hotel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));

        // ============================================
        // Restaurant — Menu removed, ImageCover added
        // ============================================
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom<RestaurantOpeningHourResolver>())
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom<RestaurantCloseHourResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom(src => src.CuisineType.ToString()));

        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<CreateRestaurantOperatingHoursResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        CreateMap<UpdateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<UpdateRestaurantOperatingHoursResolver>())
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        // ============================================
        // Landmark — Price, Rating, ImageCover added
        // Rating ignored on Create/Update (calculated from reviews)
        // ============================================
        CreateMap<Landmark, LandmarkDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom<LandmarkOpeningHourResolver>())
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom<LandmarkCloseHourResolver>());

        CreateMap<CreateLandmarkDto, Landmark>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<CreateLandmarkOperatingHoursResolver>());

        CreateMap<UpdateLandmarkDto, Landmark>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom<UpdateLandmarkOperatingHoursResolver>());

        // ============================================
        // Program — Location removed, Date→Duration, Country+ImageCover added
        // ============================================
        CreateMap<Domain.Entities.Program, ProgramDto>();

        CreateMap<CreateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UpdateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // ============================================
        // Review
        // ============================================
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.EntityType, opt => opt.MapFrom(src => src.EntityType.ToString()));

        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TouristId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.EntityType, opt => opt.MapFrom<EntityTypeResolver>());

        // ============================================
        // FavoriteList
        // ============================================
        CreateMap<FavoriteList, FavoriteDto>();

        CreateMap<FavoriteDto, FavoriteList>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }

    // ============================================
    // Value Resolvers
    // ============================================

    public class RestaurantOpeningHourResolver : IValueResolver<Restaurant, RestaurantDto, string>
    {
        public string Resolve(Restaurant source, RestaurantDto destination, string destMember, ResolutionContext context)
            => source.OperatingHours?.OpeningHour.ToString(@"hh\:mm") ?? string.Empty;
    }

    public class RestaurantCloseHourResolver : IValueResolver<Restaurant, RestaurantDto, string>
    {
        public string Resolve(Restaurant source, RestaurantDto destination, string destMember, ResolutionContext context)
            => source.OperatingHours?.ClosingHour.ToString(@"hh\:mm") ?? string.Empty;
    }

    public class CreateRestaurantOperatingHoursResolver : IValueResolver<CreateRestaurantDto, Restaurant, OperatingHours?>
    {
        public OperatingHours? Resolve(CreateRestaurantDto source, Restaurant destination, OperatingHours? destMember, ResolutionContext context)
            => MapOperatingHours(source.OpeningHour, source.ClosingHour);
    }

    public class UpdateRestaurantOperatingHoursResolver : IValueResolver<UpdateRestaurantDto, Restaurant, OperatingHours?>
    {
        public OperatingHours? Resolve(UpdateRestaurantDto source, Restaurant destination, OperatingHours? destMember, ResolutionContext context)
            => MapOperatingHours(source.OpeningHour, source.ClosingHour);
    }

    public class CuisineTypeResolver : IValueResolver<CreateRestaurantDto, Restaurant, CuisineType>,
                                       IValueResolver<UpdateRestaurantDto, Restaurant, CuisineType>
    {
        public CuisineType Resolve(CreateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
            => ParseCuisineType(source.CuisineType);

        public CuisineType Resolve(UpdateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
            => ParseCuisineType(source.CuisineType);
    }

    public class LandmarkOpeningHourResolver : IValueResolver<Landmark, LandmarkDto, string>
    {
        public string Resolve(Landmark source, LandmarkDto destination, string destMember, ResolutionContext context)
            => source.OperatingHours?.OpeningHour.ToString(@"hh\:mm") ?? string.Empty;
    }

    public class LandmarkCloseHourResolver : IValueResolver<Landmark, LandmarkDto, string>
    {
        public string Resolve(Landmark source, LandmarkDto destination, string destMember, ResolutionContext context)
            => source.OperatingHours?.ClosingHour.ToString(@"hh\:mm") ?? string.Empty;
    }

    public class CreateLandmarkOperatingHoursResolver : IValueResolver<CreateLandmarkDto, Landmark, OperatingHours?>
    {
        public OperatingHours? Resolve(CreateLandmarkDto source, Landmark destination, OperatingHours? destMember, ResolutionContext context)
            => MapOperatingHours(source.OpeningHour, source.ClosingHour);
    }

    public class UpdateLandmarkOperatingHoursResolver : IValueResolver<UpdateLandmarkDto, Landmark, OperatingHours?>
    {
        public OperatingHours? Resolve(UpdateLandmarkDto source, Landmark destination, OperatingHours? destMember, ResolutionContext context)
            => MapOperatingHours(source.OpeningHour, source.ClosingHour);
    }

    public class EntityTypeResolver : IValueResolver<CreateReviewDto, Review, FavoriteType>
    {
        public FavoriteType Resolve(CreateReviewDto source, Review destination, FavoriteType destMember, ResolutionContext context)
            => ParseFavoriteType(source.EntityType);
    }

    // ============================================
    // Helpers
    // ============================================

    private static OperatingHours? MapOperatingHours(string openingHour, string closeHour)
    {
        if (string.IsNullOrWhiteSpace(openingHour) || string.IsNullOrWhiteSpace(closeHour))
            return null;
        try
        {
            return new OperatingHours(TimeSpan.Parse(openingHour), TimeSpan.Parse(closeHour));
        }
        catch { return null; }
    }

    private static CuisineType ParseCuisineType(string cuisineType)
    {
        if (Enum.TryParse<CuisineType>(cuisineType, true, out var result))
            return result;
        return CuisineType.Other;
    }

    private static FavoriteType ParseFavoriteType(string entityType)
    {
        if (Enum.TryParse<FavoriteType>(entityType, true, out var result))
            return result;
        return FavoriteType.Hotel;
    }
}

