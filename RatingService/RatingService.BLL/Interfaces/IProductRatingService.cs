using RatingService.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Interfaces
{
    public interface IProductRatingService
    {
        Task<List<ProductRatingDto>> GetAllByProductIdAsync(
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
        Task<List<ProductRatingDto>> GetAllByUserIdAsync(
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
        Task<ProductRatingDto> CreateAsync(ProductRatingDto productRating);
        Task DeleteAsync(string userId, int productId);
        Task<ProductRatingDto> UpdateAsync(string userId, int productId, ProductRatingDto productRating);
    }
}
