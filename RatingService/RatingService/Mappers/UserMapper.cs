using RatingService.BLL.Dtos;
using RatingService.Dtos.User;

namespace RatingService.Mappers
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
