using RadioShop.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Services
{
    public interface IRatingService
    {
        public Task<List<UserProductRatingDto>> GetAllByUserIdAsync(
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            string? productName = null,
            bool isDescending = false,
            string? sortBy = null
        );
        public Task<int> CountByUserIdAsync(
            string userId,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            string? productName = null
        );
        public Task<List<ProductProductRatingDto>> GetAllByProductIdAsync(
            int productId, 
            int pageNumber = 1, 
            int pageSize = 20, 
            byte? minRating = null, 
            byte? maxRating = null, 
            DateTime? minDateTime = null, 
            DateTime? maxDateTime = null, 
            bool isDescending = false, 
            string? sortBy = null
        );
        public Task<int> CountByProductIdAsync(
            int productId,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null
        );
        public Task<UserProductRatingDto> GetByIdAsync(int productId, string userId);
        public Task<UserProductRatingDto> CreateAsync(UserProductRatingDto productRatingDto);
        public Task<UserProductRatingDto> UpdateAsync(int productId, string userId, UserProductRatingDto productRatingDto);
        public Task<UserProductRatingDto> DeleteAsync(string requestedUserId, int productId, string userId);
    }
}
