using RatingService.BLL.Dtos;
using RatingService.Dtos.Tag;

namespace RatingService.Mappers
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
