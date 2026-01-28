using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Review repository interface
/// Based on Use Case: Make Review
/// </summary>
public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetByEntityAsync(string entityId);

    Task<List<Review>> GetByTouristAsync(string touristId);
}