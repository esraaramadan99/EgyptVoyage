using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Review;

public class CreateReviewDto
{
    public string EntityId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // Hotel, Restaurant, Landmark, Program
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}