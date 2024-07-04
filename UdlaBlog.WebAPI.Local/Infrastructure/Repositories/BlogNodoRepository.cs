using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;
using UdlaBlog.Infrastructure.Data;

namespace UdlaBlog.Infrastructure.Repositories
{
    public class BlogNodoRepository : IBlogNodoRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogNodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogNodo> GetByIdAsync(Guid id)
        {
            return await _context.BlogNodos
                .Include(b => b.Tags)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BlogNodo>> GetAllAsync()
        {
            return await _context.BlogNodos
                .Include(b => b.Tags)
                .Include(b => b.Comments)
                .ToListAsync();
        }

        public async Task AddAsync(BlogNodo entity)
        {
            await _context.BlogNodos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogNodo entity)
        {
            _context.BlogNodos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.BlogNodos.FindAsync(id);
            if (entity != null)
            {
                _context.BlogNodos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
