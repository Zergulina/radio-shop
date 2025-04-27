using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IProductRatingRepository
    {
        Task<ProductRating?> GetByIdAsync(int productId, string userId);
        Task<List<ProductRating>> GetAllByUserIdAsync(
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
        Task<int> CountByUserIdAsync(
            string userId,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            string? productName = null
        );
        Task<List<ProductRating>> GetAllByProductIdAsync(
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
        Task<int> CountByProductIdAsync(
            int productId,
            byte? minRating = null,
            byte? maxRating = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null
        );
        Task<ProductRating> CreateAsync(ProductRating productRating);
        Task<ProductRating?> UpdateAsync(int productId, string userId, ProductRating productRating);
        Task<ProductRating?> DeleteAsync(int productId, string userId);
        Task<bool> ExistsAsync(int productId, string userId);
    }
}
