using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Interfaces;
using CatalogService.Dtos.Tag;
using CatalogService.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
[Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok((await _tagService.GetByIdAsync(id)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _tagService.GetAllAsync()).Select(x => x.ToResponseDto()));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequestDto createRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok((await _tagService.CreateAsync(createRequestDto.ToDto())).ToResponseDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTagRequestDto updateRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok((await _tagService.UpdateAsync(id, updateRequestDto.ToDto())).ToResponseDto());
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
                return Ok((await _tagService.DeleteAsync(id)).ToResponseDto());
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
