
/* 
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptVoyage.Application.DTOs.FavoriteList
{
    internal class FavoriteDetailDto
    {
    }
}
*/
using EgyptVoyage.Application.DTOs.Hotel;
using EgyptVoyage.Application.DTOs.Landmark;
using EgyptVoyage.Application.DTOs.Program;
using EgyptVoyage.Application.DTOs.Restaurant;

namespace EgyptVoyage.Application.DTOs.Favorite;

public class FavoriteDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string TouristId { get; set; } = string.Empty;
    public List<HotelDto> Hotels { get; set; } = new();
    public List<RestaurantDto> Restaurants { get; set; } = new();
    public List<LandmarkDto> Landmarks { get; set; } = new();
    public List<ProgramDto> Programs { get; set; } = new();
}