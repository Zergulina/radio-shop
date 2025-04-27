using RadioShop.BLL.Dtos;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto?> GetByIdAsync(string requestedUserLogin, int id);
        Task<List<OrderDto>> GetAllAsync(
            string requestedUserLogin,
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
            string requestedUserLogin,
            string userId,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            DateTime? startOrderDateTime = null,
            DateTime? endOrderDateTime = null
        );
        Task<List<OrderProductOrderDto>> GetAllProductsByOrderIdAsync(
            string requestedUserLogin,
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
        Task<int> CountProductByOrderIdAsync(
            string requestedUserLogin,
            int orderId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            ulong? minPrice = null,
            ulong? maxPrice = null,
            string? productName = null
        );
        Task<List<ProductProductOrderDto>> GetAllOrdersByProductIdAsync(
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
        Task<int> CountOrdersByProductIdAsync(
            int productId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            DateTime? minOrderDateTime = null,
            DateTime? maxOrderDateTime = null
        );
        Task<OrderDto> CreateAsync(OrderDto order);
        Task<OrderDto> DeleteAsync(int id);
    }
}
