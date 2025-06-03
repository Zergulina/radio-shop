using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.DAL.Interfaces
{
    public interface IProductRatingRepository
    {
        Task<ProductRating?> GetByIdAsync(string userId, int productId);
        Task<List<ProductRating>> GetAllByProductIdAsync(
            int productId,
            int pageNumber = 1,
            int pageSize = 20,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByProductIdAsync(
            int productId,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null
        );
        Task<List<ProductRating>> GetAllByUserIdAsync(
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            byte? minMyRating = null,
            byte? maxMyRating = null,
            byte? minRating = null,
            byte? maxRating = null,
            string? name = null,
            string? tag = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByUserIdAsync(
            string userId,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            byte? minMyRating = null,
            byte? maxMyRating = null,
            byte? minRating = null,
            byte? maxRating = null,
            string? name = null,
            string? tag = null,
            DateTime? startCreatedAt = null,
            DateTime? endCreatedAt = null
        );
        Task<ProductRating> CreateAsync( ProductRating productRating );
        Task<ProductRating?> DeleteAsync(string userId, int productId);
        Task<ProductRating?> UpdateAsync(string userId, int productId, ProductRating productRating);
        Task<bool> ExistsAsync(string userId, int productId);
    }
}
