using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Infrastructure.Repositories;


public class LandmarkRepository : Repository<Landmark>, ILandmarkRepository
{
    public LandmarkRepository(MongoDbContext context) : base(context.Landmarks)
    {
    }

    
}