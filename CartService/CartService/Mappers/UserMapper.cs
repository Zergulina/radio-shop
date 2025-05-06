using CartService.BLL.Dtos;
using CartService.Dtos.User;

namespace CartService.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto ToResponse(this UserDto dto)
        {
            return new UserResponseDto
            {
                Id = dto.Id,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
            };
        }
    }
}
