using AutoMapper;
using EgyptVoyage.Application.DTOs;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Application.DTOs.Program;
using EgyptVoyage.Application.DTOs.Restaurant;
using EgyptVoyage.Application.DTOs.Review;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Domain.Enums;
using EgyptVoyage.Domain.ValueObjects;

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
        // Hotel
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
        // Restaurant
        // ============================================
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => src.OpeningHour.ToString(@"hh\:mm")))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => src.ClosingHour.ToString(@"hh\:mm")))
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom(src => src.CuisineType.ToString()));

        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.OpeningHour)))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.ClosingHour)))
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        CreateMap<UpdateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.OpeningHour)))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.ClosingHour)))
            .ForMember(dest => dest.CuisineType, opt => opt.MapFrom<CuisineTypeResolver>());

        // ============================================
        // Landmark
        // ============================================
        CreateMap<Landmark, LandmarkDto>()
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => src.OpeningHour.ToString(@"hh\:mm")))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => src.ClosingHour.ToString(@"hh\:mm")));

        CreateMap<CreateLandmarkDto, Landmark>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.OpeningHour)))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.ClosingHour)));

        CreateMap<UpdateLandmarkDto, Landmark>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.OpeningHour)))
            .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.ClosingHour)));

        // ============================================
        // Program
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

    public class CuisineTypeResolver : IValueResolver<CreateRestaurantDto, Restaurant, CuisineType>,
                                       IValueResolver<UpdateRestaurantDto, Restaurant, CuisineType>
    {
        public CuisineType Resolve(CreateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
            => ParseCuisineType(source.CuisineType);

        public CuisineType Resolve(UpdateRestaurantDto source, Restaurant destination, CuisineType destMember, ResolutionContext context)
            => ParseCuisineType(source.CuisineType);
    }

    public class EntityTypeResolver : IValueResolver<CreateReviewDto, Review, FavoriteType>
    {
        public FavoriteType Resolve(CreateReviewDto source, Review destination, FavoriteType destMember, ResolutionContext context)
            => ParseFavoriteType(source.EntityType);
    }

    // ============================================
    // Helpers
    // ============================================

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

