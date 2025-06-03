using System;
using CatalogService.DAL.Data;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Repositories;

internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(decimal? minPrice = null, decimal? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null)
        {
            var products = _context.Products.Include(x => x.Tags).AsQueryable();

            if (minPrice != null)
            {
                products = products.Where(x => x.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                products = products.Where(x => x.Price <= maxPrice);
            }
            if (minRating != null)
            {
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) <= maxRating);
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

        public async Task<List<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 20, decimal? minPrice = null, decimal? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null, bool isDescending = false, string? sortBy = null)
        {
            var products = _context.Products.Include(x => x.Tags).AsQueryable();

            if (minPrice != null)
            {
                products = products.Where(x => x.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                products = products.Where(x => x.Price <= maxPrice);
            }
            if (minRating != null)
            {
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) <= maxRating);
            }
            if (name != null)
            {
                products = products.Where(x => x.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (tag != null)
            {
                products = products.Where(x => x.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => x.Price) : products.OrderBy(x => x.Price);
                }
                else if (string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0)) : products.OrderBy(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0));
                }
                else if (string.Equals(sortBy, "name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isDescending ? products.OrderByDescending(x => x.Name) : products.OrderBy(x => x.Name);
                }
            }

            return await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Tags).FirstAsync(x => x.Id == id);
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
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<Product?> AddRatingAsync(int id, byte rating)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.TotalRating += rating;
            existingProduct.RatingAmount++;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product?> RemoveRatingAsync(int id, byte rating)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.TotalRating -= rating;
            existingProduct.RatingAmount--;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product?> AddOrderAmountAsync(int id, ulong orderAmount)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.OrderAmount+= orderAmount;
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<Product?> RemoveOrderAmountAsync(int id, ulong orderAmount)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.OrderAmount -= orderAmount;
            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
