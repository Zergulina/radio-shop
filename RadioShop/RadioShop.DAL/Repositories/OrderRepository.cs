using Microsoft.EntityFrameworkCore;
using RadioShop.DAL.Data;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync(string userId, ulong? minPrice = null, ulong? maxPrice = null, DateTime? startOrderDateTime = null, DateTime? endOrderDateTime = null)
        {
            var orders = _context.Orders.Where(x => x.UserId.Equals(userId)).Include(x => x.ProductOrders).ThenInclude(x => x.Product).AsQueryable();

            if (minPrice != null)
            {
                orders = orders.Where(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                orders = orders.Where(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100) <= maxPrice);
            }
            if (startOrderDateTime != null)
            {
                orders = orders.Where(x => x.OrderDateTime >= startOrderDateTime);
            }
            if (endOrderDateTime != null)
            {
                orders = orders.Where(x => x.OrderDateTime <= endOrderDateTime);
            }

            return await orders.CountAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (existingOrder == null) {
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

        public async Task<List<Order>> GetAllAsync(string userId, int pageNumber = 1, int pageSize = 20, ulong? minPrice = null, ulong? maxPrice = null, DateTime? startOrderDateTime = null, DateTime? endOrderDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var orders = _context.Orders.Where(x => x.UserId.Equals(userId)).Include(x => x.ProductOrders).ThenInclude(x => x.Product).AsQueryable();

            if (minPrice != null)
            {
                orders = orders.Where(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                orders = orders.Where(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100) <= maxPrice);
            }
            if (startOrderDateTime != null)
            {
                orders = orders.Where(x => x.OrderDateTime >= startOrderDateTime);
            }
            if (endOrderDateTime != null)
            {
                orders = orders.Where(x => x.OrderDateTime <= endOrderDateTime);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100)) : orders.OrderBy(x => x.ProductOrders.Sum(x => (decimal)x.Product.PriceRuble + (decimal)x.Product.PriceKopek * 100));
                }
                if (string.Equals(sortBy, "orderDateTime"))
                {
                    orders = isDescending ? orders.OrderByDescending(x => x.OrderDateTime) : orders.OrderBy(x => x.OrderDateTime);
                }
            }

            return await _context.Orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();   
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
