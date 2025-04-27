using Microsoft.EntityFrameworkCore;
using RadioShop.DAL.Data;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Repositories
{
    internal class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(int id)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTag == null)
            {
                return null;
            }
            _context.Tags.Remove(existingTag);
            await _context.SaveChangesAsync();
            return existingTag;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tags.AnyAsync(x => x.Id == id);
        }


        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(int id, Tag tag)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTag == null)
            {
                return null;
            }

            existingTag.Name = tag.Name;
            
            await _context.SaveChangesAsync();
            return tag;
        }
    }
}
