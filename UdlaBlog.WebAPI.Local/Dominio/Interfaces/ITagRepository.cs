using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.WebAPI.Local.Domain.Models;

namespace UdlaBlog.WebAPI.Local.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag> GetByIdAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Guid id);
    }
}
