using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(int productId, string userId);
        Task<List<Cart>> GetAllByProductIdAsync(
            int productId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByProductIdAsync(
             int productId,
             ulong? minAmount = null,
             ulong? maxAmount = null,
             DateTime? startDateTime = null,
             DateTime? endDateTime = null
         );
        Task<List<Cart>> GetAllByUserIdAsync(
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByUserIdAsync(
            string userId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? startDateTime = null,
            DateTime? endDateTime = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null
        );
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart?> UpdateAsync(int productId, string userId, Cart cart);
        Task<Cart?> DeleteAsync(int productId, string userId);
        Task DeleteAllByUserIdAsync(string userId);
        Task<bool> ExistsAsync(int productId, string userId);
    }
}
