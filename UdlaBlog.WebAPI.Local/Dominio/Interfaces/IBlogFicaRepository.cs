using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;

namespace UdlaBlog.Domain.Interfaces
{
    public interface IBlogFicaRepository
    {
        Task<BlogFica> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogFica>> GetAllAsync();
        Task AddAsync(BlogFica entity);
        Task UpdateAsync(BlogFica entity);
        Task DeleteAsync(Guid id);
    }
}
