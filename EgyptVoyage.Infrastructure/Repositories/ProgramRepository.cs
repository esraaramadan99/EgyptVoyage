using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Infrastructure.Repositories;

/// <summary>
/// Program repository implementation
/// </summary>
public class ProgramRepository : Repository<Program>, IProgramRepository
{
    public ProgramRepository(MongoDbContext context) : base(context.Programs)
    {
    }
}