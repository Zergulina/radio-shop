using Microsoft.EntityFrameworkCore;
using OrderService.DAL.Data;
using OrderService.DAL.Interfaces;
using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Repositories
{
    internal class OrderUnitRepository : IOrderUnitRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderUnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountByOrderIdAsync(
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
            )
        {
            var orderUnits = _context.OrderUnits.Where(x => x.OrderId == orderId).Include(x => x.Product).AsQueryable();
            
            if (minAmount != null)
            {
                orderUnits = orderUnits.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                orderUnits = orderUnits.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price <= maxPrice);
            }
            if (minCost != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price * x.Amount >= minCost);
            }
            if (maxCost != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price * x.Amount <= maxCost);
            }
            if (minRating != null)
            {
                orderUnits = orderUnits.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                orderUnits = orderUnits.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) <= maxRating);
            }
            if (name != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Name.Contains(name));
            }
            if (tag != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }

            return await orderUnits.CountAsync();
        }

        public async Task<List<OrderUnit>> GetAllByOrderIdAsync(
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
        )
        {
            var orderUnits = _context.OrderUnits.Where(x => x.OrderId == orderId).Include(x => x.Product).AsQueryable();

            if (minAmount != null)
            {
                orderUnits = orderUnits.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                orderUnits = orderUnits.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price <= maxPrice);
            }
            if (minCost != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price * x.Amount >= minCost);
            }
            if (maxCost != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Price * x.Amount <= maxCost);
            }
            if (minRating != null)
            {
                orderUnits = orderUnits.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) >= minRating);
            }
            if (maxRating != null)
            {
                orderUnits = orderUnits.Where(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0) <= maxRating);
            }
            if (name != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Name.Contains(name));
            }
            if (tag != null)
            {
                orderUnits = orderUnits.Where(x => x.Product.Tags.Select(x => x.Name).Any(tagName => tag.Contains(tagName)));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    orderUnits = isDescending ? orderUnits.OrderByDescending(x => x.Product.Price) : orderUnits.OrderBy(x => x.Product.Price);
                }
                else if (string.Equals(sortBy, "cost", StringComparison.OrdinalIgnoreCase))
                {
                    orderUnits = isDescending ? orderUnits.OrderByDescending(x => x.Product.Price * x.Amount) : orderUnits.OrderBy(x => x.Product.Price * x.Amount);
                }
                else if (string.Equals(sortBy, "amount", StringComparison.OrdinalIgnoreCase))
                {
                    orderUnits = isDescending ? orderUnits.OrderByDescending(x => x.Amount) : orderUnits.OrderBy(x => x.Amount);
                }
                else if (string.Equals(sortBy, "rating", StringComparison.OrdinalIgnoreCase))
                {
                    orderUnits = isDescending ? orderUnits.OrderByDescending(x => (x.Product.RatingAmount > 0 ? x.Product.TotalRating / x.Product.RatingAmount : 0)) : orderUnits.OrderBy(x => x.Product.TotalRating / x.Product.RatingAmount);
                }
                else if (string.Equals(sortBy, "name", StringComparison.OrdinalIgnoreCase))
                {
                    orderUnits = isDescending ? orderUnits.OrderByDescending(x => x.Product.Name) : orderUnits.OrderBy(x => x.Product.Name);
                }
            }

            return await orderUnits.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
