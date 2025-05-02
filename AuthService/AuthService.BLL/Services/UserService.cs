using System;
using AuthService.BLL.Dtos;
using AuthService.BLL.Exceptions;
using AuthService.BLL.Interfaces.Helpers;
using AuthService.BLL.Interfaces.Services;
using AuthService.BLL.Mappers;
using AuthService.DAL.Interfaces;

namespace AuthService.BLL.Services;

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

        public async Task<List<UserDto>> GetByIdsAsync(List<string> ids)
        {
            var users = await _userRepository.GetByIdsAsync(ids);
            return users.Select(x => x.ToDto()).ToList();
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

        public async Task<bool> ExistsAsync(string id)
        {
            return await _userRepository.ExistsAsync(id);
        }

        public async Task<List<string>> ExistsByIdsAsync(List<string> ids)
        {
            return await _userRepository.ExistsByIdsAsync(ids);
        }
}