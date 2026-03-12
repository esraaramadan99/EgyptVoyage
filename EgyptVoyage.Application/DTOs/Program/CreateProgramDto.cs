using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EgyptVoyage.Application.DTOs.Program;

public class CreateProgramDto
{
    public string Name { get; set; } = string.Empty;

    
    public double DurationValue { get; set; }

    
    public string DurationUnit { get; set; } = "Days";

  
    public TimeSpan Duration => DurationUnit.ToLower() switch
    {
        "hours" => TimeSpan.FromHours(DurationValue),
        _ => TimeSpan.FromDays(DurationValue)
    };

    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();
    public string ImageCover { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
