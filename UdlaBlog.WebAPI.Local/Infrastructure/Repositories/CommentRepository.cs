using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;
using UdlaBlog.Infrastructure.Data;

namespace UdlaBlog.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAllByBlogFicaIdAsync(Guid blogFicaId)
        {
            return await _context.Comments
                .Where(c => c.BlogFicaId == blogFicaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByBlogNodoIdAsync(Guid blogNodoId)
        {
            return await _context.Comments
                .Where(c => c.BlogNodoId == blogNodoId)
                .ToListAsync();
        }

        public async Task AddAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity != null)
            {
                _context.Comments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
