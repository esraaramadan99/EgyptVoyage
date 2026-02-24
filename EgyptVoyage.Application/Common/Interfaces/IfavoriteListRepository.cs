using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;

public interface IFavoriteListRepository : IRepository<FavoriteList>
{
    Task<FavoriteList?> GetByTouristIdAsync(string touristId);
    Task<FavoriteList> AddItemAsync(string touristId, string entityType, string entityId);
    Task<bool> RemoveItemAsync(string touristId, string entityType, string entityId);
}