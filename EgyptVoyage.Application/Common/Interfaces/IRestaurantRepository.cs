using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Restaurant repository interface
/// Based on Analysis: Issue Query Subsystem
/// </summary>
public interface IRestaurantRepository : IRepository<Restaurant>
{
    // من Analysis: searchRestaurantByName(name)
    Task<List<Restaurant>> SearchByNameAsync(string name);

    // getAllRestaurants() → موجود في IRepository.GetAllAsync()
    // getAllRestaurantDetails() → نفس GetByIdAsync()
}