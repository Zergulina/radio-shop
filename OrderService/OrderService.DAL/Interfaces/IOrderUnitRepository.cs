using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Interfaces
{
    public interface IOrderUnitRepository
    {
        Task<List<OrderUnit>> GetAllByOrderIdAsync(
            int orderId,
            int pageNumber = 1,
            int pageSize = 20,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            decimal? minCost = null,
            decimal? maxCost = null,
            byte? minRating = null,
            byte? maxRating = null,
            string? name = null,
            string? tag = null,
            bool isDescending = false,
            string? sortBy = null
        );
        Task<int> CountByOrderIdAsync(
            int orderId,
            ulong? minAmount = null,
            ulong? maxAmount = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            decimal? minCost = null,
            decimal? maxCost = null,
            byte? minRating = null,
            byte? maxRating = null,
            string? name = null,
            string? tag = null
        );
    }
}
