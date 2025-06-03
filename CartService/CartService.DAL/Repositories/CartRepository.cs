using CartService.DAL.Data;
using CartService.DAL.Interfaces;
using CartService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL.Repositories
{
    internal class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        async public Task<Cart> CreateAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return await _context.Carts
                .Include(x => x.Product)
                .ThenInclude(x => x.Tags)
                .FirstAsync(x => x.ProductId == cart.ProductId && x.UserId.Equals(cart.UserId));
        }

        public async Task<bool> DeleteAsync(string userId, int productId)
        {
            
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
            if (cart == null)
            {
                return false;
            }

            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.Carts.AnyAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
        }

        public async Task<List<Cart>> GetAllByProductIdAsync(int? pageNumber, int? pageSize, int productId)
        {
            var carts = _context.Carts.Where(x => x.ProductId.Equals(productId)).AsQueryable();
            if (pageNumber != null && pageSize != null)
            {
                carts = carts.Skip(((int)pageNumber + 1) * (int)pageSize).Take((int)pageSize);
            }
            return await carts.ToListAsync();
        }

        public async Task<int> CountByProductIdAsync(int productId)
        {
            return await _context.Carts.Where(x => x.ProductId == productId).CountAsync();
        }

        public async Task<List<Cart>> GetAllByUserIdAsync(int? pageNumber, int? pageSize, string userId)
        {
            var carts = _context.Carts.Where(x => x.UserId.Equals(userId)).AsQueryable();
            if (pageNumber != null && pageSize != null)
            {
                carts = carts.Skip(((int)pageNumber + 1) * (int)pageSize).Take((int)pageSize);
            }
            return await carts.Include(x => x.Product).ThenInclude(x => x.Tags).ToListAsync();
        }

        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.Carts.Where(x => x.UserId.Equals(userId)).CountAsync();
        }

        public async Task<Cart?> GetByIdAsync(string userId, int productId)
        {
            return await _context.Carts.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ProductId == productId);
        }

        public async Task<Cart?> UpdateAsync(string userId, int productId, Cart cart)
        {
            var existingCart = await _context.Carts.Include(x => x.Product).ThenInclude(x => x.Tags).FirstOrDefaultAsync(x => x.UserId.Equals(userId) && productId == x.ProductId);
            if  (existingCart == null)
            {
                return null;
            }
            existingCart.Amount = cart.Amount;
            await _context.SaveChangesAsync();
            return existingCart;
        }
    }
}
