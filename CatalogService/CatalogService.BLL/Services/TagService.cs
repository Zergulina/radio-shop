using System;
using CatalogService.BLL.Dtos;
using CatalogService.BLL.Exceptions;
using CatalogService.BLL.Interfaces;
using CatalogService.BLL.Mappers;
using CatalogService.DAL.Interfaces;

namespace CatalogService.BLL.Services;

internal class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<TagDto> CreateAsync(TagDto dto)
        {
            return (await _tagRepository.CreateAsync(dto.ToModel())).ToDto();
        }

        public async Task<TagDto> DeleteAsync(int id)
        {
            var tag = await _tagRepository.DeleteAsync(id);
            if (tag ==  null)
            {
                throw new NotFoundException("Tag not found");
            }
            return tag.ToDto();
        }

        public async Task<List<TagDto>> GetAllAsync()
        {
            return (await _tagRepository.GetAllAsync()).Select(x => x.ToDto()).ToList();
        }

        public async Task<TagDto> GetByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
            {
                throw new NotFoundException("Tag not found");
            }
            return tag.ToDto();
        }

        public async Task<TagDto> UpdateAsync(int id, TagDto dto)
        {
            var tag = await _tagRepository.UpdateAsync(id, dto.ToModel());
            if (tag == null)
            {
                throw new NotFoundException("Tag not found");
            }
            return tag.ToDto();
        }
    }