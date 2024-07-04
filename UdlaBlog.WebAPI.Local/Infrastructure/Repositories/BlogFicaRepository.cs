using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;
using UdlaBlog.Infrastructure.Data;

namespace UdlaBlog.Infrastructure.Repositories
{
    public class BlogFicaRepository : IBlogFicaRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogFicaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogFica> GetByIdAsync(Guid id)
        {
            return await _context.BlogFicas
                .Include(b => b.Tags)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BlogFica>> GetAllAsync()
        {
            return await _context.BlogFicas
                .Include(b => b.Tags)
                .Include(b => b.Comments)
                .ToListAsync();
        }

        public async Task AddAsync(BlogFica entity)
        {
            await _context.BlogFicas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogFica entity)
        {
            _context.BlogFicas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.BlogFicas.FindAsync(id);
            if (entity != null)
            {
                _context.BlogFicas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
