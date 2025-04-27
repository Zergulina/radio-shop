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
    internal class ProductTagRepository : IProductTagRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag?> CreateAsync(int tagId, int productId)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if (existingTag == null)
            {
                return null;
            }
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Tags.Add(existingTag);
            await _context.SaveChangesAsync();

            return existingTag;
        }

        public async Task<Tag?> DeleteAsync(int tagId, int productId)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if (existingTag == null)
            {
                return null;
            }
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Tags.Remove(existingTag);
            await _context.SaveChangesAsync();

            return existingTag;
        }

        public async Task<bool> ExistsAsync(int tagId, int productId)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if (existingTag == null)
            {
                return false;
            }
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (existingProduct == null)
            {
                return false;
            }

            return existingProduct.Tags.Contains(existingTag);
        }

        public async Task<List<Tag>> GetAllTagsByProductIdAsync(int productId)
        {
            var product = await _context.Products.Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null)
            {
                return new List<Tag>();
            }

            return product.Tags;
        }
    }
}
