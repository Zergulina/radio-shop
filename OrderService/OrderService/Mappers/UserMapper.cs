using OrderService.BLL.Dtos;
using OrderService.Dtos.User;

namespace OrderService.Mappers
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
