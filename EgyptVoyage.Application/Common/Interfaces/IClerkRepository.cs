using EgyptVoyage.Domain.Entities;

namespace EgyptVoyage.Application.Common.Interfaces;

// Interface بتحدد العمليات المتاحة على الـ Clerk في الـ Database
public interface IClerkRepository : IRepository<Clerk>
{
    // بنجيب الـ Clerk بالإيميل بتاعه عشان نستخدمها في الـ Login
    Task<Clerk?> GetByEmailAsync(string email);
}