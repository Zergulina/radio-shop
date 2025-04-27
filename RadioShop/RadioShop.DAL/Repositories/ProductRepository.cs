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
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null)
        {
            var products = _context.Products.Include(x => x.ProductRatings).Include(x => x.Tags).Include(x => x.Image).AsQueryable();

            if (minPrice != null)
            {
                products = products.Where(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                products = products.Where(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100) <= maxPrice);
            }
            if (minRating != null)
            {
                products = products.Where(x => x.ProductRatings.Average(x => x.Rating) >= minRating);
            }
            if (maxRating != null)
            {
                products = products.Where(x => x.ProductRatings.Average(x => x.Rating) <= maxRating);
            }
            if (name != null)
            {
                products = products.Where(x => x.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (tag != null)
            {
                products = products.Where(x => x.Tags.Select(x => x.Name).Any(tagName => tag.ToUpper().Contains(tagName.ToUpper())));
            }

            return await products.CountAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 20, ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null, bool isDescending = false, string? sortBy = null)
        {
            var products = _context.Products.Include(x => x.ProductRatings).Include(x => x.Tags).Include(x=>x.Image).AsQueryable();

            if (minPrice != null)
            {
                products = products.Where(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                products = products.Where(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100) <= maxPrice);
            }
            if (minRating != null)
            {
                products = products.Where(x => x.ProductRatings.Average(x => x.Rating) >= minRating);
            }
            if (maxRating != null)
            {
                products = products.Where(x => x.ProductRatings.Average(x => x.Rating) <= maxRating);
            }
            if (name != null)
            {
                products = products.Where(x => x.Name.Contains(name));
            }
            if (tag != null)
            {
                products = products.Where(x => x.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100)) : products.OrderBy(x => ((ulong)(x.PriceRuble) + (ulong)(x.PriceKopek) * 100));
                }
                if (string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => x.ProductRatings.Average(x => x.Rating)) : products.OrderBy(x => x.ProductRatings.Average(x => x.Rating));
                }
                if (string.Equals(sortBy, "name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => x.Name) : products.OrderBy(x => x.Name);
                }
            }

            return await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.ProductRatings).Include(x => x.Tags).Include(x => x.Image).FirstAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.PriceRuble = product.PriceRuble;
            existingProduct.PriceKopek = product.PriceKopek;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}
