using RadioShop.BLL.Dtos;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Helpers;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.BLL.Mappers;
using RadioShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public UserService(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<int> CountAsync(string? name = null)
        {
            return await _userRepository.CountAsync(name);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            if (await _userRepository.GetByUserNameAsync(userDto.UserName) != null)
            {
                throw new UserNameIsOccupiedException();
            }
            var user = await _userRepository.CreateAsync(userDto.ToModel(), userDto.Password);
            if (user == null)
            {
                throw new UserCreationException();
            }
            return user.ToDto(userDto.Password);
        }

        public async Task<UserDto> DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user = await _userRepository.DeleteAsync(id);
            return user.ToDto();
        }

        public async Task<List<UserDto>> GetAllAsync(int pageNumber = 1, int pageSize = 20, string? name = null, string sortBy = "", bool isDescending = false)
        {
            var users = await _userRepository.GetAllAsync(pageNumber, pageSize, name, sortBy, isDescending);
            return users.Select(x => x.ToDto()).ToList();
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return user.ToDto();
        }

        public async Task<(string, IList<string>)> LoginAsync(UserDto userDto)
        {
            var user = await _userRepository.GetByUserNameAsync(userDto.UserName);
            if (user == null)
            {
                throw new IncorrectUserNameOrPasswordException();
            }
            var roles = await _userRepository.GetRolesAsync(user);
            if (await _userRepository.CheckUserNamePassword(userDto.UserName, userDto.Password))
                return (_tokenService.CreateToken(user.ToDto(), roles), roles);
            else throw new IncorrectUserNameOrPasswordException();
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto, string requestedUserId)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null )
            {
                throw new NotFoundException("User not found");
            }
            user = await _userRepository.UpdateAsync(userDto.ToModel());
            return user.ToDto();
        }
    }
}
