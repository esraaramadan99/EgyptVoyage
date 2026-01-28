using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Favorite;

public class AddToFavoriteDto
{
    public string EntityId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // Hotel, Restaurant, Landmark, Program
}