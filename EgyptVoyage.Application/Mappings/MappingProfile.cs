/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

namespace EgyptVoyage.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Location mappings
        CreateMap<Location, LocationDto>().ReverseMap();

        // Auth mappings
        CreateMap<RegisterDto, Tourist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Hotel mappings
        CreateMap<Hotel, HotelDto>();
        CreateMap<CreateHotelDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateHotelDto, Hotel>();

        // Restaurant mappings
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateRestaurantDto, Restaurant>();

        // Landmark mappings
        CreateMap<Landmark, LandmarkDto>();
        CreateMap<CreateLandmarkDto, Landmark>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateLandmarkDto, Landmark>();
        // Program mappings
        CreateMap<Domain.Entities.Program, ProgramDto>();
        CreateMap<CreateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateProgramDto, Domain.Entities.Program>();

        // Review mappings
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TouristId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // FavoriteList mappings
        CreateMap<FavoriteList, FavoriteDto>();
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
        // Location mappings
        CreateMap<Location, LocationDto>().ReverseMap();

        // Auth mappings
        CreateMap<RegisterDto, Tourist>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Hotel mappings
        CreateMap<Hotel, HotelDto>();
        CreateMap<CreateHotelDto, Hotel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));
        CreateMap<UpdateHotelDto, Hotel>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (HotelLevel)src.Level));

        // Restaurant mappings
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateRestaurantDto, Restaurant>();

        // Landmark mappings
        CreateMap<Landmark, LandmarkDto>();
        CreateMap<CreateLandmarkDto, Landmark>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateLandmarkDto, Landmark>();

        // Program mappings
        CreateMap<Domain.Entities.Program, ProgramDto>();
        CreateMap<CreateProgramDto, Domain.Entities.Program>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<UpdateProgramDto, Domain.Entities.Program>();

        // Review mappings
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TouristId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // FavoriteList mappings
        CreateMap<FavoriteList, FavoriteDto>();
    }
}