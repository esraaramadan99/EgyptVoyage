// المسار: EgyptVoyage.Application/Common/Interfaces/IfavoriteListRepository.cs

using EgyptVoyage.Application.DTOs.Favorite;
using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;

public interface IFavoriteListRepository : IRepository<FavoriteList>
{
    Task<FavoriteList?> GetByTouristIdAsync(string touristId);
    // Task<FavoriteDto> GetByTouristIdWithDetailsAsync(string touristId);
    Task<FavoriteDetailDto> GetByTouristIdWithDetailsAsync(string touristId);
    Task<FavoriteList> AddItemAsync(string touristId, string entityType, string entityId);
    Task<bool> RemoveItemAsync(string touristId, string entityType, string entityId);
}
