using Microsoft.EntityFrameworkCore;
using RatingService.DAL.Data;
using RatingService.DAL.Interfaces;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.DAL.Repositories
{
    internal class ProductRatingRepository : IProductRatingRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountByProductIdAsync(
            int productId, 
            byte? minRating = null, 
            byte? maxRating = null, 
            DateTime? startCreatedAt = null, 
            DateTime? endCreatedAt = null
        )
        {
            var productRatings = _context.ProductRatings.Where(x => x.ProductId == productId).AsQueryable();
            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (startCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt >= startCreatedAt);
            }
            if (endCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt <= endCreatedAt);
            }

            return await productRatings.CountAsync();
        }

        public async Task<int> CountByUserIdAsync(
            string userId,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            byte? minRating = null,
            byte? maxRating = null,
            byte? minMyRating = null,
            byte? maxMyRating = null,
            string? name = null,
            string? tag = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null
        )
        {
            var productRatings = _context.ProductRatings.Where(x => x.UserId.Equals(userId)).Include(x => x.Product).ThenInclude(x => x.Tags).AsQueryable();
            if (minPrice != null)
            {
                productRatings = productRatings.Where(x => x.Product.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                productRatings = productRatings.Where(x => x.Product.Price <= maxPrice);
            }
            if (minRating != null)
            {
                productRatings = productRatings.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) <= maxRating);
            }
            if (minMyRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxMyRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (name != null)
            {
                productRatings = productRatings.Where(x => x.Product.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (tag != null)
            {
                productRatings = productRatings.Where(x => x.Product.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }
            if (startCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt >= startCreatedAt);
            }
            if (endCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt <= endCreatedAt);
            }

            return await productRatings.CountAsync();
        }

        public async Task<ProductRating> CreateAsync(ProductRating productRating)
        {
            await _context.ProductRatings.AddAsync(productRating);
            await _context.SaveChangesAsync();

            return await _context.ProductRatings.Include(x => x.Product).ThenInclude(x => x.Tags).FirstAsync(x => x.UserId.Equals(productRating.UserId) && x.ProductId == productRating.ProductId);
        }

        public async Task<ProductRating?> DeleteAsync(string userId, int productId)
        {
            var existsingProductRating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
            if (existsingProductRating == null)
            {
                return null;
            }

            _context.ProductRatings.Remove(existsingProductRating);
            await _context.SaveChangesAsync();
            return existsingProductRating;
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.ProductRatings.AnyAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
        }

        public async Task<List<ProductRating>> GetAllByProductIdAsync(
            int productId, 
            int pageNumber = 1, 
            int pageSize = 20, 
            byte? minRating = null, 
            byte? maxRating = null, 
            DateTime? startCreatedAt = null, 
            DateTime? endCreatedAt = null, 
            bool isDescending = false, 
            string? sortBy = null
        )
        {
            var productRatings = _context.ProductRatings.Where(x => x.ProductId == productId).AsQueryable();
            if (minRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (startCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt >= startCreatedAt);
            }
            if (endCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt <= endCreatedAt);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Rating) : productRatings.OrderBy(x => x.Rating);
                }
                else if (string.Equals(sortBy, "createdAt", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.CreatedAt) : productRatings.OrderBy(x => x.CreatedAt);
                }
            }

            return await productRatings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<ProductRating>> GetAllByUserIdAsync(
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            byte? minRating = null,
            byte? maxRating = null,
            byte? minMyRating = null,
            byte? maxMyRating = null,
            string? name = null,
            string? tag = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null,
            bool isDescending = false,
            string? sortBy = null
        )
        {
            var productRatings = _context.ProductRatings.Where(x => x.UserId.Equals(userId)).Include(x => x.Product).AsQueryable();
            if (minPrice != null)
            {
                productRatings = productRatings.Where(x => x.Product.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                productRatings = productRatings.Where(x => x.Product.Price <= maxPrice);
            }
            if(minRating != null)
            {
                productRatings = productRatings.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                productRatings = productRatings.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) <= maxRating);
            }
            if (minMyRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating >= minRating);
            }
            if (maxMyRating != null)
            {
                productRatings = productRatings.Where(x => x.Rating <= maxRating);
            }
            if (name != null)
            {
                productRatings = productRatings.Where(x => x.Product.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (tag != null)
            {
                productRatings = productRatings.Where(x => x.Product.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }
            if (startCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt >= startCreatedAt);
            }
            if (endCreatedAt != null)
            {
                productRatings = productRatings.Where(x => x.CreatedAt <= endCreatedAt);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Product.Price) : productRatings.OrderBy(x => x.Product.Price);
                }
                else if (string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0)) : productRatings.OrderBy(x => x.Product.TotalRating / x.Product.RatingAmount);
                }
                else if (string.Equals(sortBy, "myRating", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Rating) : productRatings.OrderBy(x => x.Rating);
                }
                else if (string.Equals(sortBy, "name", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.Product.Name) : productRatings.OrderBy(x => x.Product.Name);
                }
                else if (string.Equals(sortBy, "createdAt", StringComparison.OrdinalIgnoreCase))
                {
                    productRatings = isDescending ? productRatings.OrderByDescending(x => x.CreatedAt) : productRatings.OrderBy(x => x.CreatedAt);
                }
            }

            return await productRatings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<ProductRating?> GetByIdAsync(string userId, int productId)
        {
            return await _context.ProductRatings.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
        }

        public async Task<ProductRating?> UpdateAsync(string userId, int productId, ProductRating productRating)
        {
            var rating = await _context.ProductRatings.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
            if (rating == null)
            {
                return null;
            }
            rating.Rating = productRating.Rating;
            rating.Comment = productRating.Comment;
            await _context.SaveChangesAsync();
            return rating;
        }
    }
}
