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

        [Route("{imageId}")]
        public async IActionResult GetById([FromRoute] string imageId)
        {
            var file = await _productImageService.GetByIdAsync(imageId);
            return File(file.ImageData, file.)
        }
    }
}
