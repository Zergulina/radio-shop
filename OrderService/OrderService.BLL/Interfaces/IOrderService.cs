using OrderService.BLL.Dtos;
using OrderService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(OrderDto dto);
        Task<OrderDto> DeleteAsync(int id);
        Task<OrderDto> DeleteAcceptedAsync(int id, string userId);
        Task<OrderDto> GetByIdAsync(int id);
        Task<List<OrderDto>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string? userId = null,
            DateTime? startOrderedAt = null,
            DateTime? endOrderedAt = null,
            DateTime? startDeliveryDateTime = null,
            DateTime? endDeliveryDateTime = null,
            ulong? minUnitsAmount = null,
            ulong? maxUnitsAmount = null,
            decimal? minCost = null,
            decimal? maxCost = null,
            bool? isAccepted = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountAsync(
            string? userId = null,
            DateTime? startOrderedAt = null,
            DateTime? endOrderedAt = null,
            DateTime? startDeliveryDateTime = null,
            DateTime? endDeliveryDateTime = null,
            ulong? minUnitsAmount = null,
            ulong? maxUnitsAmount = null,
            decimal? minCost = null,
            decimal? maxCost = null,
            bool? isAccepted = null
        );
        Task AcceptAsync(int id, string userId);
        Task<bool> CheckDoesUserBoughtProduct(string userId, int productId);
    }
}
