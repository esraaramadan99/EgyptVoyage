using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs.Program;


public class UpdateProgramDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();

    public string ImageCover { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}