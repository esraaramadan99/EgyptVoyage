using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Landmark;

public class UpdateLandmarkDto
{
    public string Id { get; set; } = string.Empty;
    public string LandmarkName { get; set; } = string.Empty;
    public string OpeningHour { get; set; } = string.Empty;
    public string ClosingHour { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
}