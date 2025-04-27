using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(int pageNumber = 1, int pageSize = 20, string? name = null, string sortBy = "", bool isDescending = false);
        Task<User?> GetByIdAsync(string id);
        Task<User?> CreateAsync(User user, string password);
        Task<User?> UpdateAsync(User user);
        Task<User?> DeleteAsync(string id);
        Task<bool> CheckUserNamePassword(string userName, string password);
        Task<User?> GetByUserNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(User user);
        Task<bool> AddRoleAsync(User user, string role);
        Task<bool> ExistsAsync(string id);
        Task<int> CountAsync(string? name = null);
    }
}
