using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;





public interface IHotelRepository : IRepository<Hotel>
{
    Task<List<Hotel>> SearchByNameAsync(string name);

}
