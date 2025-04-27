using RadioShop.BLL.Dtos;
using RadioShop.WEB.Dtos.User;

namespace RadioShop.WEB.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this LoginDto loginDto)
        {
            return new UserDto()
            {
                UserName = loginDto.UserName,
                Password = loginDto.Password,
            };
        }

        public static UserDto ToUserDto(this CreateUserRequestDto createUserRequestDto)
        {
            return new UserDto
            {
                UserName = createUserRequestDto.UserName,
                Password = createUserRequestDto.Password,
                FirstName = createUserRequestDto.FirstName,
                LastName = createUserRequestDto.LastName,
                MiddleName = createUserRequestDto.MiddleName,
                Email = createUserRequestDto.Email,
            };
        }

        public static UserDto ToUserDto(this UpdateUserRequestDto updateUserRequestDto)
        {
            return new UserDto
            {
                FirstName = updateUserRequestDto.FirstName,
                LastName = updateUserRequestDto.LastName,
                MiddleName = updateUserRequestDto.MiddleName,
                Email = updateUserRequestDto.Email,
            };
        }

        public static AuthorizedUserDto ToAuthorizedFromLoginDto(this LoginDto loginDto, string token, IList<string> roles)
        {
            return new AuthorizedUserDto
            {
                Token = token,
                UserName = loginDto.UserName,
                Roles = roles
            };
        }

        public static CreatedUserDto ToCreatedFromUserDto(this UserDto userDto)
        {
            return new CreatedUserDto
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                MiddleName = userDto.MiddleName,
                Email= userDto.Email,
            };
        }

        public static UserResponseDto ToResponseFromUserDto(this UserDto userDto)
        {
            return new UserResponseDto
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                MiddleName = userDto.MiddleName,
                Email = userDto.Email,
            };
        }
    }
}
