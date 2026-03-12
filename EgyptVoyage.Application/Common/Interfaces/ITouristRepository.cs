using EgyptVoyage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace EgyptVoyage.Application.Common.Interfaces;


public interface ITouristRepository : IRepository<Tourist>
{
    Task<Tourist?> GetByEmailAsync(string email);

    Task<bool> EmailExistsAsync(string email);

    Task<Tourist?> GetByResetTokenAsync(string token);

}