using RadioShop.BLL.Dtos;
using RadioShop.WEB.Dtos.Image;

namespace RadioShop.WEB.Mappers
{
    public static class ImageMapper
    {
        public static ImageDto ToDto(this CreateImageRequestDto createImageRequestDto)
        {
            return new ImageDto
            {
                ImageData = createImageRequestDto.ImageData,
                ImageExtention = createImageRequestDto.ImageExtention,
            };
        }

        public static ImageResponseDto ToResponseDto(this ImageDto imageDto)
        {
            return new ImageResponseDto
            {
                Id = imageDto.Id,
                ImageData = imageDto.ImageData,
                ImageExtention = imageDto.ImageExtention,
            };
        }
    }
}
