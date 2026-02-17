using EgyptVoyage.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Domain.Entities;


public class FavoriteList : BaseEntity
{
    public string TouristId { get; set; } = string.Empty;
    public List<string> HotelIds { get; set; } = new();
    public List<string> RestaurantIds { get; set; } = new();
    public List<string> LandmarkIds { get; set; } = new();
    public List<string> ProgramIds { get; set; } = new();
}

