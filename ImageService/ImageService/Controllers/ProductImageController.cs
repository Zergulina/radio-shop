using ImageService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [Route("api/products/images")]
    public class ProductImageController : ControllerBase
    {
        IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet("{imageId}")]
        public async Task<IActionResult> GetById([FromRoute] string imageId)
        {
            var file = await _productImageService.GetByIdAsync(imageId);
            var extention = "";
            extention = file.ImageType switch
            {
                "image/jpeg" => ".jpeg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                _ => extention
            };
            return File(file.ImageData, file.ImageType, $"{imageId}.{extention}");
        }
    }
}