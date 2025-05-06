using CartService.BLL.Dtos;
using CartService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetByIdAsync(string userId, int productId);
        Task<List<CartDto>> GetAllByUserIdAsync(int? pageNumber, int? pageSize, string userId);
        Task<int> CountByUserIdAsync(string userId);
        Task<List<CartDto>> GetAllByProductIdAsync(int? pageNumber, int? pageSize, int productId);
        Task<int> CountByProductIdAsync(int productId);
        Task<CartDto> CreateAsync(CartDto cart);
        Task DeleteAsync(string userId, int productId);
        Task<bool> ExistsAsync(string userId, int productId);
    }
}
