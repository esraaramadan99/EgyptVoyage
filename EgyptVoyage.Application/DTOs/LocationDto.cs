using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.DTOs;

/// <summary>
/// Shared location DTO
/// </summary>
public class LocationDto
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
   // public double? Latitude { get; set; }
   // public double? Longitude { get; set; }
}