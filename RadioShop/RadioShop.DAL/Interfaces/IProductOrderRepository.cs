using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IProductOrderRepository
    {
        Task<bool> CheckUserBoughtProduct(string userId, int productId);
        Task<ProductOrder?> GetByIdAsync(int orderId, int productId);
        Task<List<ProductOrder>> GetAllByOrderIdAsync(
            int orderId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByOrderIdAsync(
             int orderId,
             ulong? minAmount = null,
             ulong? maxAmount = null,
             ulong? minPrice = null,
             ulong? maxPrice = null,
             string? productName = null
         );
        Task<List<ProductOrder>> GetAllByProductIdAsync(
            int productId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? minOrderDateTime = null,
            DateTime? maxOrderDateTime = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByProductIdAsync(
            int productId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? minOrderDateTime = null,
            DateTime? maxOrderDateTime = null
        );
        Task<ProductOrder> CreateAsync(ProductOrder productOrder);
        Task<ProductOrder?> DeleteAsync(int productId, int orderId);
        Task<bool> ExistsAsync(int productId, int orderId);
    }
}
