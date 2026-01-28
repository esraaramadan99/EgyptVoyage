using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Tourist repository interface
/// Based on Use Cases: Sign Up, Login
/// </summary>
public interface ITouristRepository : IRepository<Tourist>
{
    Task<Tourist?> GetByEmailAsync(string email);

    Task<bool> EmailExistsAsync(string email);
}