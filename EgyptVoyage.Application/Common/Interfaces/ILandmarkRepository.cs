using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Landmark repository interface
/// Based on Analysis: Issue Query Subsystem
/// </summary>
public interface ILandmarkRepository : IRepository<Landmark>
{
    // من Analysis: searchLandmarkByName(name)
    Task<List<Landmark>> SearchByNameAsync(string name);

    // getAllLandmarks() → موجود في IRepository.GetAllAsync()
    // getLandmarkById(landmarkId) → موجود في IRepository.GetByIdAsync()
}