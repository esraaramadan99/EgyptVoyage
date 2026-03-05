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


public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(MongoDbContext context) : base(context.Hotels)
    {
    }

 
}