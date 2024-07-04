using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;

namespace UdlaBlog.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag> GetByIdAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task AddAsync(Tag entity);
        Task UpdateAsync(Tag entity);
        Task DeleteAsync(Guid id);
    }
}
