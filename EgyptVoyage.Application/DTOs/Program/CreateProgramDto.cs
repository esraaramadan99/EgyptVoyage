using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptVoyage.Application.DTOs.Program;

public class CreateProgramDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public LocationDto Location { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}