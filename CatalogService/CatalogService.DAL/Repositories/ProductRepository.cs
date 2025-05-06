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

        public async Task<int> CountAsync(ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null)
        {
            var products = _context.Products.Include(x => x.Tags).AsQueryable();

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

        public async Task<List<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 20, ulong? minPrice = null, ulong? maxPrice = null, byte? minRating = null, byte? maxRating = null, string? name = null, string? tag = null, bool isDescending = false, string? sortBy = null)
        {
            var products = _context.Products.Include(x => x.Tags).AsQueryable();

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
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                products = products.Where(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0) <= maxRating);
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
                    products = isDescending ? products.OrderByDescending(x => (x.RatingAmount > 0 ? x.TotalRating / x.RatingAmount : 0)) : products.OrderBy(x => x.TotalRating / x.RatingAmount);
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
            existingProduct.PriceRuble = product.PriceRuble;
            existingProduct.PriceKopek = product.PriceKopek;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<Product?> AddRating(int id, byte rating)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.TotalRating += rating;
            existingProduct.RatingAmount++;

            return existingProduct;
        }

        public async Task<Product?> RemoveRating(int id, byte rating)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.TotalRating -= rating;
            existingProduct.RatingAmount--;

            return existingProduct;
        }

        public async Task<Product?> IncreaseOrderAmountAsync(int id)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.OrderAmount++;

            return existingProduct;
        }
        public async Task<Product?> DecreaseOrderAmountAsync(int id)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.OrderAmount--;

            return existingProduct;
        }
    }
