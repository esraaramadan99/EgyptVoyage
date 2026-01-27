using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using EgyptVoyage.Domain.Entities.Common;
using EgyptVoyage.Domain.Enums;

namespace EgyptVoyage.Domain.Entities;

/// <summary>
/// Clerk entity for admin users
/// </summary>
public class Clerk : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Clerk;
}