using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RatingService.BLL.Exceptions;
using RatingService.BLL.Interfaces;
using RatingService.Dtos.ProductRating;
using RatingService.Mappers;
using RatingService.Queries.ProductRating;

namespace RatingService.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        private readonly IProductRatingService _productRatingService;
        public ProductRatingController(IProductRatingService productRatingService)
        {
            _productRatingService = productRatingService;
        }
        [Authorize]
        [HttpPost("products/{productId:int}/rating")]
        public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateProductRatingRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }
            try 
            {
                var rating = await _productRatingService.CreateAsync(createDto.ToDto(userId, productId));
                return Ok(rating.ToResponse());
            }
            catch (UnauthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (AlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpDelete("products/{productId:int}/rating")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }
            try
            {
                await _productRatingService.DeleteAsync(userId, productId);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPut("products/{productId:int}/rating")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] UpdateProductRatingRequestDto updateDto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                return Unauthorized("User Id is not valid");
            }
            try
            {
                var rating = await _productRatingService.UpdateAsync(userId, productId, updateDto.ToDto());
                return Ok(rating.ToResponse());
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("products/{productId:int}/rating")]
        public async Task<IActionResult> GetAllByProductId([FromRoute] int productId, [FromQuery] GetAllByProductIdQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var carts = await _productRatingService.GetAllByProductIdAsync(
                    productId, 
                    query.PageNumber,
                    query.PageSize,
                    query.MinRating,
                    query.MaxRating,
                    query.StartCreatedAt,
                    query.EndCreatedAt,
                    query.IsDescending,
                    query.SortBy
                );
                return Ok(carts);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("products/{productId:int}/rating/count")]
        public async Task<IActionResult> CountByProductId([FromRoute] int productId, [FromQuery] CountByProductIdQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var count = await _productRatingService.CountByProductIdAsync(
                    productId,
                    query.MinRating,
                    query.MaxRating,
                    query.StartCreatedAt,
                    query.EndCreatedAt
                );
                return Ok(count);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("users/{userId}/products-rating")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] string userId, [FromQuery] GetAllByUserIdQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var carts = await _productRatingService.GetAllByUserIdAsync(
                    userId,
                    query.PageNumber,
                    query.PageSize,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinMyRating,
                    query.MaxMyRating,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag,
                    query.StartCreatedAt,
                    query.EndCreatedAt,
                    query.IsDescending,
                    query.SortBy
                );
                return Ok(carts);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("users/{userId}/products-rating/count")]
        public async Task<IActionResult> CountByUserId([FromRoute] string userId, [FromQuery] CountByUserIdQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var count = await _productRatingService.CountByUserIdAsync(
                    userId,
                    query.MinPrice,
                    query.MaxPrice,
                    query.MinMyRating,
                    query.MaxMyRating,
                    query.MinRating,
                    query.MaxRating,
                    query.Name,
                    query.Tag,
                    query.StartCreatedAt,
                    query.EndCreatedAt
                );
                return Ok(count);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
