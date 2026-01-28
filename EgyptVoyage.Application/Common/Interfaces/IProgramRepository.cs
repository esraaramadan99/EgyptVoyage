using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Common.Interfaces;

/// <summary>
/// Program repository interface
/// Based on Analysis: Issue Query Subsystem
/// </summary>
public interface IProgramRepository : IRepository<Program>
{
    // getAllPrograms() → موجود في IRepository.GetAllAsync()
    // getProgramDetails() → موجود في IRepository.GetByIdAsync()

}