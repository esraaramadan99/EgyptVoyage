using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// FavoriteList repository interface
/// Based on Use Case: Add to Favorites, View Favorites
/// </summary>
public interface IFavoriteListRepository : IRepository<FavoriteList>
{
    Task<FavoriteList?> GetByTouristIdAsync(string touristId);
}