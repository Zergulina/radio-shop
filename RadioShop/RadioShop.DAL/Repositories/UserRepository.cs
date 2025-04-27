using Microsoft.AspNetCore.Identity;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RadioShop.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<User>> GetAllAsync(int pageNumber = 1, int pageSize = 20, string? name = null, string sortBy = "", bool isDescending = false)
        {
            var users = _userManager.Users.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(x => x.FirstName.ToUpper().Contains(name.ToUpper()) || x.LastName.ToUpper().Contains(name.ToUpper()) || x.MiddleName != null ? x.MiddleName.ToUpper().Contains(name.ToUpper()) : false);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    users = isDescending ? users.OrderByDescending(x => x.FirstName + x.LastName + x.MiddleName) : users.OrderBy(x => x.FirstName + x.LastName + x.MiddleName);
                }
            }

            return await users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<User?> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return null;
            }
            return user;
        }
        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            if (existingUser == null)
            {
                return null;
            }
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (existingUser == null)
            {
                return null;
            }
            await _userManager.DeleteAsync(existingUser);
            return existingUser;
        }

        public async Task<bool> CheckUserNamePassword(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return false;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> AddRoleAsync(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _userManager.Users.AnyAsync(x => x.Id.Equals(id));
        }
        public async Task<int> CountAsync(string? name = null)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(x => x.FirstName.ToUpper().Contains(name.ToUpper()) || x.LastName.ToUpper().Contains(name.ToUpper()) || x.MiddleName != null ? x.MiddleName.ToUpper().Contains(name.ToUpper()) : false);
            }

            return await users.CountAsync();
        }
    }
}
