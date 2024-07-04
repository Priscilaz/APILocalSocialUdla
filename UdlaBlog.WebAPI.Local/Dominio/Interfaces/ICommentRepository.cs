using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;

namespace UdlaBlog.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllByBlogFicaIdAsync(Guid blogFicaId);
        Task<IEnumerable<Comment>> GetAllByBlogNodoIdAsync(Guid blogNodoId);
        Task AddAsync(Comment entity);
        Task UpdateAsync(Comment entity);
        Task DeleteAsync(Guid id);
    }
}
