using CartService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(string userId, int productId);
        Task<List<Cart>> GetAllByUserIdAsync(int? pageNumber, int? pageSize, string userId);
        Task<int> CountByUserIdAsync(string userId);
        Task<List<Cart>> GetAllByProductIdAsync(int? pageNumber, int? pageSize, int productId);
        Task<int> CountByProductIdAsync(int productId);
        Task<Cart?> CreateAsync(Cart cart);
        Task<bool> DeleteAsync(string userId, int productId);
        Task<bool> ExistsAsync(string userId, int productId);
    }
}
