using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadioShop.BLL.Exceptions;
using RadioShop.BLL.Interfaces.Services;
using RadioShop.WEB.Dtos.Image;
using RadioShop.WEB.Dtos.Product;
using RadioShop.WEB.Mappers;
using RadioShop.WEB.Queries.Product;

namespace RadioShop.WEB.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok((await _productService.GetByIdAsync(id)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductGetAllQuery query)
        {
            try
            {
                return Ok((await _productService.GetAllAsync(query.PageNumber, query.PageSize, query.MinPrice, query.MaxPrice, query.MinRating, query.MaxRating, query.Name, query.Tag, query.IsDescending, query.SortBy)).Select(x => x.ToResponseDto()).ToList());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] ProductCountQuery query)
        {
            try
            {
                return Ok(await _productService.CountAsync(query.MinPrice, query.MaxPrice, query.MinRating, query.MaxRating, query.Name, query.Tag));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto createRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok((await _productService.CreateAsync(createRequestDto.ToDto())).ToResponseDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok((await _productService.CreateAsync(updateRequestDto.ToDto())).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                return Ok((await _productService.DeleteAsync(id)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}/tags")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTags([FromRoute] int id, [FromBody] TagRequestDto tagsRequestDto)
        {
            try
            {
                return Ok((await _productService.AddTagsAsync(id, tagsRequestDto.TagIds)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}/tags")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTags([FromRoute] int id, [FromBody] TagRequestDto tagsRequestDto)
        {
            try
            {
                return Ok((await _productService.RemoveTagsAsync(id, tagsRequestDto.TagIds)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateImage([FromRoute] int id, [FromBody] CreateImageRequestDto createRequestDto )
        {
            try
            {
                return Ok((await _productService.UpdateImageByProductId(id, createRequestDto.ToDto())).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            try
            {
                return Ok((await _productService.DeleteImageByProductId(id)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
