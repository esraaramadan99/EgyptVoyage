using EgyptVoyage.Domain.Entities.Common;

namespace EgyptVoyage.Domain.Entities;


public class Clerk : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}