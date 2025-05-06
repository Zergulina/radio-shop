using CartService.BLL.Dtos;
using CartService.Dtos.Tag;

namespace CartService.Mappers
{
    public static class TagMapper
    {
        public static TagResponseDto ToResponse(this TagDto dto)
        {
            return new TagResponseDto
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
