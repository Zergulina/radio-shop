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
    internal class ProductOrderRepository : IProductOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUserBoughtProduct(string userId, int productId)
        {
            return await _context.Orders.Where(x => x.UserId.Equals(userId)).SelectMany(x => x.ProductOrders).AnyAsync(x => x.ProductId == productId);
        }

        public async Task<int> CountByOrderIdAsync(int orderId, ulong? minAmount = null, ulong? maxAmount = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null)
        {
            var productOrders = _context.ProductOrders.Where(x => x.OrderId == orderId).Include(x => x.Product).AsQueryable();

            if (minAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                productOrders = productOrders.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                productOrders = productOrders.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) <= minPrice);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                productOrders = productOrders.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }

            return await productOrders.CountAsync();
        }

        public async Task<int> CountByProductIdAsync(int productId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null)
        {
            var productOrders = _context.ProductOrders.Where(x=> x.ProductId == productId).Include(x => x.Order).AsQueryable();

            if (minAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount <= maxAmount);
            }
            if (minOrderDateTime != null)
            {
                productOrders = productOrders.Where(x => x.Order.OrderDateTime >= minOrderDateTime);
            }
            if (maxOrderDateTime != null)
            {
                productOrders = productOrders.Where(x => x.Order.OrderDateTime >= maxOrderDateTime);
            }

            return await productOrders.CountAsync();
        }

        public async Task<ProductOrder> CreateAsync(ProductOrder productOrder)
        {
            await _context.ProductOrders.AddAsync(productOrder);
            await _context.SaveChangesAsync();
            return productOrder;
        }

        public async Task<ProductOrder?> DeleteAsync(int productId, int orderId)
        {
            var existingProductOrder = await _context.ProductOrders.FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == orderId);
            if (existingProductOrder == null)
            {
                return null;
            }

            _context.ProductOrders.Remove(existingProductOrder);
            await _context.SaveChangesAsync();
            return existingProductOrder;
        }

        public async Task<bool> ExistsAsync(int productId, int orderId)
        {
            return await _context.ProductOrders.AnyAsync(x => x.ProductId==productId && x.OrderId==orderId);
        }

        public async Task<List<ProductOrder>> GetAllByOrderIdAsync(int orderId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var productOrders = _context.ProductOrders.Where(x => x.OrderId == orderId).Include(x => x.Product).AsQueryable();

            if (minAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                productOrders = productOrders.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) >= minPrice);
            }
            if (maxPrice != null)
            {
                productOrders = productOrders.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) <= minPrice);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                productOrders = productOrders.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (!string.Equals(sortBy, "amount", StringComparison.OrdinalIgnoreCase))
                {
                    productOrders = isDescending ? productOrders.OrderByDescending(x => x.Amount) : productOrders.OrderBy(x => x.Amount);
                }
                if (!string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    productOrders = isDescending ? productOrders.OrderByDescending(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100)) : productOrders.OrderBy(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100));
                }
                if (!string.Equals(sortBy, "productName"))
                {
                    productOrders = isDescending ? productOrders.OrderByDescending(x => x.Product.Name) : productOrders.OrderBy(x => x.Product.Name);
                }
            }

            return await productOrders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<ProductOrder>> GetAllByProductIdAsync(int productId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var productOrders = _context.ProductOrders.Where(x => x.ProductId == productId).Include(x => x.Order).AsQueryable();

            if (minAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                productOrders = productOrders.Where(x => x.Amount <= maxAmount);
            }
            if (minOrderDateTime != null)
            {
                productOrders = productOrders.Where(x => x.Order.OrderDateTime >= minOrderDateTime);
            }
            if (maxOrderDateTime != null)
            {
                productOrders = productOrders.Where(x => x.Order.OrderDateTime >= maxOrderDateTime);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (!string.Equals(sortBy, "amount", StringComparison.OrdinalIgnoreCase))
                {
                    productOrders = isDescending ? productOrders.OrderByDescending(x => x.Amount) : productOrders.OrderBy(x => x.Amount);
                }
                if (!string.Equals(sortBy, "orderDateTime"))
                {
                    productOrders = isDescending ? productOrders.OrderByDescending(x => x.Order.OrderDateTime) : productOrders.OrderBy(x => x.Order.OrderDateTime);
                }
            }

            return await productOrders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<ProductOrder?> GetByIdAsync(int orderId, int productId)
        {
            return await _context.ProductOrders.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        }
    }
}
