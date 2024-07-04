using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;

namespace UdlaBlog.Domain.Interfaces
{
    public interface IBlogNodoRepository
    {
        Task<BlogNodo> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogNodo>> GetAllAsync();
        Task AddAsync(BlogNodo entity);
        Task UpdateAsync(BlogNodo entity);
        Task DeleteAsync(Guid id);
    }
}
