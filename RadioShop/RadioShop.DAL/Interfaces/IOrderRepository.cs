using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetAllAsync(
            string userId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            DateTime? startOrderDateTime = null,
            DateTime? endOrderDateTime = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountAsync(
            string userId,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            DateTime? startOrderDateTime = null,
            DateTime? endOrderDateTime = null
        );
        Task<Order> CreateAsync(Order order);
        Task<Order?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
