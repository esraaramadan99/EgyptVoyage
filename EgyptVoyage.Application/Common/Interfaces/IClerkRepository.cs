using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Clerk repository interface
/// Based on Use Case: Clerk Login, Maintain Basic Data
/// </summary>
public interface IClerkRepository : IRepository<Clerk>
{
    Task<Clerk?> GetByEmailAsync(string email);

    //Task<bool> EmailExistsAsync(string email);
}