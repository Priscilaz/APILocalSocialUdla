using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdlaBlog.WebAPI.Local.Domain.Interfaces;
using UdlaBlog.WebAPI.Local.Domain.Models;
using UdlaBlog.WebAPI.Local.Infrastructure.Data.Context;

namespace UdlaBlog.WebAPI.Local.Infrastructure.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _context.Tags.Include(t => t.BlogPosts).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(t => t.BlogPosts).ToListAsync();
        }

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
