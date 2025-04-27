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
    internal class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountByProductIdAsync(int productId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var carts = _context.Carts.Where(x => x.ProductId == productId).Include(x => x.User).AsQueryable();

            if (minAmount != null)
            {
                carts = carts.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                carts = carts.Where(x => x.Amount <= maxAmount);
            }
            if (startDateTime != null)
            {
                carts = carts.Where(x => x.DateTime >= startDateTime);
            }
            if (endDateTime != null)
            {
                carts = carts.Where(x => x.DateTime <= endDateTime);
            }

            return await carts.CountAsync();
        }

        public async Task<int> CountByUserIdAsync(string userId, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null)
        {
            var carts = _context.Carts.Where(x => x.UserId.Equals(userId)).Include(x => x.Product).AsQueryable();

            if (minAmount != null)
            {
                carts = carts.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                carts = carts.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                carts = carts.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) >= minPrice);
            }
            if (startDateTime != null)
            {
                carts = carts.Where(x => x.DateTime >= startDateTime);
            }
            if (endDateTime != null)
            {
                carts = carts.Where(x => x.DateTime <= endDateTime);
            }
            if (maxPrice != null)
            {
                carts = carts.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) <= minPrice);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                carts = carts.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }

            return await carts.CountAsync();
        }

        public async Task<Cart> CreateAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task DeleteAllByUserIdAsync(string userId)
        {
            var carts = await _context.Carts.Where(x => x.UserId.Equals(userId)).ToListAsync();
            _context.Carts.RemoveRange(carts);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> DeleteAsync(int productId, string userId)
        {
            var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId.Equals(userId));
            if (existingCart == null)
            {
                return null;
            }

            _context.Carts.Remove(existingCart);
            await _context.SaveChangesAsync();
            return existingCart;
        }

        public async Task<bool> ExistsAsync(int productId, string userId)
        {
            return await _context.Carts.AnyAsync(x => x.ProductId == productId && x.UserId.Equals(userId));
        }

        public async Task<List<Cart>> GetAllByProductIdAsync(int productId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, bool isDescending = false, string? sortBy = null)
        {
            var carts = _context.Carts.Where(x => x.ProductId == productId).Include(x => x.User).AsQueryable();

            if (minAmount != null)
            {
                carts = carts.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                carts = carts.Where(x => x.Amount <= maxAmount);
            }
            if (startDateTime != null)
            {
                carts = carts.Where(x => x.DateTime >= startDateTime);
            }
            if (endDateTime != null)
            {
                carts = carts.Where(x => x.DateTime <= endDateTime);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "amount", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => x.Amount) : carts.OrderBy(x => x.Amount);
                }
                if (string.Equals(sortBy, "dateTime", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => x.DateTime) : carts.OrderBy(x => x.DateTime);
                }
            }

            return await carts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Cart>> GetAllByUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20, ulong? minAmount = null, ulong? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null, ulong? minPrice = null, ulong? maxPrice = null, string? productName = null, bool isDescending = false, string? sortBy = null)
        {
            var carts = _context.Carts.Where(x => x.UserId.Equals(userId)).Include(x => x.Product).ThenInclude(x => x.Tags).Include(x => x.Product.Image).AsQueryable();

            if (minAmount != null)
            {
                carts = carts.Where(x => x.Amount >= minAmount);
            }
            if (maxAmount != null)
            {
                carts = carts.Where(x => x.Amount <= maxAmount);
            }
            if (minPrice != null)
            {
                carts = carts.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) >= minPrice);
            }
            if (startDateTime != null)
            {
                carts = carts.Where(x => x.DateTime >= startDateTime);
            }
            if (endDateTime != null)
            {
                carts = carts.Where(x => x.DateTime <= endDateTime);
            }
            if (maxPrice != null)
            {
                carts = carts.Where(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100) <= minPrice);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                carts = carts.Where(x => x.Product.Name.ToUpper().Contains(productName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "amount", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => x.Amount) : carts.OrderBy(x => x.Amount);
                }
                if (string.Equals(sortBy, "dateTime", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => x.DateTime) : carts.OrderBy(x => x.DateTime);
                }
                if (string.Equals(sortBy, "price", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100)) : carts.OrderBy(x => ((ulong)(x.Product.PriceRuble) + (ulong)(x.Product.PriceKopek) * 100));
                }
                if (string.Equals(sortBy, "productName", StringComparison.OrdinalIgnoreCase))
                {
                    carts = isDescending ? carts.OrderByDescending(x => x.Product.Name) : carts.OrderBy(x => x.Product.Name);
                }
            }

            return await carts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(int productId, string userId)
        {
            return await _context.Carts.Include(x => x.Product).ThenInclude(x => x.Tags).Include(x => x.Product.Image).Include(x => x.User).FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId.Equals(userId));
        }

        public async Task<Cart?> UpdateAsync(int productId, string userId, Cart cart)
        {
            var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId.Equals(userId));
            if (existingCart == null)
            {
                return null;
            }

            existingCart.Amount = cart.Amount;

            await _context.SaveChangesAsync();
            return existingCart;
        }
    }
}
