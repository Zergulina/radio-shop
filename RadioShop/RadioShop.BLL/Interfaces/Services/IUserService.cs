using RadioShop.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<(string, IList<string>)> LoginAsync(UserDto userDto);
        Task<List<UserDto>> GetAllAsync(int pageNumber = 1, int pageSize = 20, string? name = null, string sortBy = "", bool isDescending = false);
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto, string requestedUserId);
        Task<UserDto> DeleteAsync(string id);
        Task<int> CountAsync(string? name = null);
    }
}
