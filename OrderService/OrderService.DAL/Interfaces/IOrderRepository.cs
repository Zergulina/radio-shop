using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Interfaces
{
    internal interface IOrderRepository
    {
       Task<Order> CreateAsync(Order order);
       Task<Order?> DeleteAsync(int id);
       Task<Order?> GetByIdAsync(int id);
       Task<List<Order>> GetAllAsync(
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
        Task<bool> ExistsAsync(int id);

    }
}
