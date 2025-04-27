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
    internal class ProductRatingRepository : IProductRatingRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRatingRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountByProductIdAsync(int productId, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null)
        {
            var productRatings = _context.ProductRatings.Where(x => x.ProductId == productId).Include(x => x.User).AsQueryable();

            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (minDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime >= minDateTime);
            }
            if (maxDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime <= maxDateTime);
            }

            return await productRatings.CountAsync();
        }

        public async Task<int> CountByUserIdAsync(string userId, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, string? productName = null)
        {
            var productRatings = _context.ProductRatings.Where(x => x.UserId == userId).Include(x => x.Product).AsQueryable();

            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (minDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime >= minDateTime);
            }
            if (maxDateTime != null) {
                productRatings = productRatings.Where(x => x.DateTime <= maxDateTime);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                productRatings = productRatings.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }

            return await productRatings.CountAsync();
        }

        public async Task<ProductRating> CreateAsync(ProductRating productRating)
        {
            await _context.ProductRatings.AddAsync(productRating);
            await _context.SaveChangesAsync();
            return productRating;
        }

        public async Task<ProductRating?> DeleteAsync(int productId, string userId)
        {
            var existingProductRating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId.Equals(userId));
            if (existingProductRating == null)
            {
                return null;
            }

            _context.ProductRatings.Remove(existingProductRating);
            await _context.SaveChangesAsync();
            return existingProductRating;
        }

        public async Task<bool> ExistsAsync(int productId, string userId)
        {
            return await _context.ProductRatings.AnyAsync(x => x.ProductId == productId && x.UserId == userId);
        }

        public async Task<List<ProductRating>> GetAllByProductIdAsync(int productId, int pageNumber = 1, int pageSize = 20, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var productRatings = _context.ProductRatings.Where(x => x.ProductId == productId).Include(x => x.Product).AsQueryable();

            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (minDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime >= minDateTime);
            }
            if (maxDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime <= maxDateTime);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (!string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending? productRatings.OrderByDescending(x => x.Rating): productRatings.OrderBy(x => x.Rating);
                }
                if (!string.Equals(sortBy, "dateTime"))
                {
                    productRatings = isDescending? productRatings.OrderByDescending(x => x.DateTime) : productRatings.OrderBy(x => x.DateTime);
                }
            }

            return await productRatings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<ProductRating>> GetAllByUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20, byte? minRating = null, byte? maxRating = null, DateTime? minDateTime = null, DateTime? maxDateTime = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var productRatings = _context.ProductRatings.Where(x => x.UserId == userId).Include(x => x.Product).AsQueryable();

            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (minDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime >= minDateTime);
            }
            if (maxDateTime != null)
            {
                productRatings = productRatings.Where(x => x.DateTime <= maxDateTime);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                productRatings = productRatings.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (!string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Rating) : productRatings.OrderBy(x => x.Rating);
                }
                if (!string.Equals(sortBy, "dateTime"))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.DateTime) : productRatings.OrderBy(x => x.DateTime);
                }
                if (!string.Equals(sortBy, "productName"))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Product.Name) : productRatings.OrderBy(x => x.Product.Name);
                }
            }

            return await productRatings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<ProductRating?> GetByIdAsync(int productId, string userId)
        {
            return await _context.ProductRatings.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
        }

        public async Task<ProductRating?> UpdateAsync(int productId, string userId, ProductRating productRating)
        {
            var existingProductRating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
            if (existingProductRating == null)
            {
                return null;
            }

            existingProductRating.Rating = productRating.Rating;
            existingProductRating.Description = productRating.Description;
            return existingProductRating;
        }
    }
}
