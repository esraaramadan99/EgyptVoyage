using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Application.Common.Interfaces;


public interface ILandmarkRepository : IRepository<Landmark>
{
   
    Task<List<Landmark>> SearchByNameAsync(string name);

}
