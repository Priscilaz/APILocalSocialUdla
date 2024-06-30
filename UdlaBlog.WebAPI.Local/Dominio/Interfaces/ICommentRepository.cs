using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.WebAPI.Local.Domain.Models;

namespace UdlaBlog.WebAPI.Local.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllByBlogPostIdAsync(Guid blogPostId);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Guid id);
    }
}
