using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Favorite;

public class FavoriteDto
{
    public string Id { get; set; } = string.Empty;
    public string TouristId { get; set; } = string.Empty;
    public List<string> HotelIds { get; set; } = new();
    public List<string> RestaurantIds { get; set; } = new();
    public List<string> LandmarkIds { get; set; } = new();
    public List<string> ProgramIds { get; set; } = new();
}