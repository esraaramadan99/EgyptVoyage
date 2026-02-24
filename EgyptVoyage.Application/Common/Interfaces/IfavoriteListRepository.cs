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

    //Task<string> GenerateShareTokenAsync(string touristId);
  //  Task<bool> RevokeShareTokenAsync(string touristId);


}