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
    internal class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AcceptAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return null;
            }

            order.IsAccepted = true;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> CheckDoesUserBoughtProduct(string userId, int productId)
        {
            return await _context.Orders.Where(x => x.UserId.Equals(userId) && x.IsAccepted).SelectMany(x => x.Units).AnyAsync(x => x.ProductId == productId);
        }

        public async Task<int> CountAsync(
            string? userId = null, 
            DateTime? startOrderedAt = null, 
            DateTime? endOrderedAt = null, 
            DateTime? startDeliveryDateTime = null, 
            DateTime? endDeliveryDateTime = null,
            ulong? minUnitsAmount = null,
            ulong? maxUnitsAmount = null,
            decimal? minCost = null,
            decimal? maxCost = null,
            bool? isAccepted = null)
        {
            var orders = _context.Orders.Include(x => x.Units).ThenInclude(x => x.Product).AsQueryable();

            if (userId != null)
            {
                orders = orders.Where(x => x.UserId.Equals(userId));
            }
            if (startOrderedAt != null)
            {
                orders = orders.Where(x => x.OrderedAt >= startOrderedAt);
            }
            if (endOrderedAt != null)
            {
                orders = orders.Where(x => x.OrderedAt <= endOrderedAt);
            }
            if (startDeliveryDateTime != null)
            {
                orders = orders.Where(x => x.DeliveryDateTime >=  startDeliveryDateTime);
            }
            if (endDeliveryDateTime != null)
            {
                orders = orders.Where(x => x.DeliveryDateTime <= endDeliveryDateTime);
            }
            if (minUnitsAmount != null)
            {
                orders = orders.Where(x => (ulong)x.Units.Count() >= minUnitsAmount);
            }
            if (maxUnitsAmount != null)
            {
                orders = orders.Where(x => (ulong)x.Units.Count() <= maxUnitsAmount);
            }
            if (minCost != null)
            {
                orders = orders.Where(x => x.Units.Sum(x => x.Product.Price * x.Amount) >= minCost);
            }
            if (maxCost != null)
            {
                orders = orders.Where(x => x.Units.Sum(x => x.Product.Price * x.Amount) <= maxCost);
            }
            if (isAccepted != null)
            {
                orders = orders.Where(x => x.IsAccepted == isAccepted);
            }

            return await orders.CountAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return await _context.Orders.Include(x => x.Units).ThenInclude(x => x.Product).FirstAsync(x => x.Id == order.Id);
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var existingOrder = await _context.Orders.Include(x => x.Units).FirstOrDefaultAsync(x => x.Id == id);
            if (existingOrder == null)
            {
                return null;
            }

            _context.Orders.Remove(existingOrder);
            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetAllAsync(
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
        )
        {
            var orders = _context.Orders.Include(x => x.Units).ThenInclude(x => x.Product).AsQueryable();

            if (userId != null)
            {
                orders = orders.Where(x => x.UserId.Equals(userId));
            }
            if (startOrderedAt != null)
            {
                orders = orders.Where(x => x.OrderedAt >= startOrderedAt);
            }
            if (endOrderedAt != null)
            {
                orders = orders.Where(x => x.OrderedAt <= endOrderedAt);
            }
            if (startDeliveryDateTime != null)
            {
                orders = orders.Where(x => x.DeliveryDateTime >= startDeliveryDateTime);
            }
            if (endDeliveryDateTime != null)
            {
                orders = orders.Where(x => x.DeliveryDateTime <= endDeliveryDateTime);
            }
            if (minUnitsAmount != null)
            {
                orders = orders.Where(x => (ulong)x.Units.Count() >= minUnitsAmount);
            }
            if (maxUnitsAmount != null)
            {
                orders = orders.Where(x => (ulong)x.Units.Count() <= maxUnitsAmount);
            }
            if (minCost != null)
            {
                orders = orders.Where(x => x.Units.Sum(x => x.Product.Price * x.Amount) >= minCost);
            }
            if (maxCost != null)
            {
                orders = orders.Where(x => x.Units.Sum(x => x.Product.Price * x.Amount) <= maxCost);
            }
            if (isAccepted != null)
            {
                orders = orders.Where(x => x.IsAccepted == isAccepted);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "orderedAt", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.OrderedAt) : orders.OrderBy(x => x.OrderedAt);
                }
                else if (string.Equals(sortBy, "deliveryDatetime", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.DeliveryDateTime) : orders.OrderBy(x => x.DeliveryDateTime);
                }
                else if (string.Equals(sortBy, "unitsAmount", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.Units.Count()) : orders.OrderBy(x => x.Units.Count());
                }
                else if (string.Equals(sortBy, "cost", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.Units.Sum(x => x.Product.Price * x.Amount)) : orders.OrderByDescending(x => x.Units.Sum(x => x.Product.Price * x.Amount));                }
                else if (string.Equals(sortBy, "isAccepted", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.IsAccepted) : orders.OrderBy(x => x.IsAccepted);
                }
            }

            return await orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(x => x.Units).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
