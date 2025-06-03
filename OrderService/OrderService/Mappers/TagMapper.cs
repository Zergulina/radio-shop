using OrderService.BLL.Dtos;
using OrderService.Dtos.Tag;

namespace OrderService.Mappers
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
