using System;
using AuthService.BLL.Dtos;

namespace AuthService.BLL.Interfaces.Services;

    public interface IUserService
    {
        Task<(string, IList<string>)> LoginAsync(UserDto userDto);
        Task<List<UserDto>> GetAllAsync(int pageNumber = 1, int pageSize = 20, string? name = null, string sortBy = "", bool isDescending = false);
        Task<UserDto> GetByIdAsync(string id);
        Task<List<UserDto>> GetByIdsAsync(List<string> ids);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto, string requestedUserId);
        Task<UserDto> DeleteAsync(string id);
        Task<int> CountAsync(string? name = null);
        Task<bool> ExistsAsync(string id);
        Task<List<string>> ExistsByIdsAsync(List<string> ids);
    }
