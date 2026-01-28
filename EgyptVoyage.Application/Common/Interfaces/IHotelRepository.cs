using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;




/// <summary>
/// Hotel repository interface
/// Based on Analysis: Issue Query Subsystem
/// </summary>
public interface IHotelRepository : IRepository<Hotel>
{
    // من Analysis: searchHotelByName(name)
    Task<List<Hotel>> SearchByNameAsync(string name);

    // getAllHotels() → موجود في IRepository.GetAllAsync()
    // getHotelById(hotelId) → موجود في IRepository.GetByIdAsync()
}
