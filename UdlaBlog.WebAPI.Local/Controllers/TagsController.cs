using Microsoft.AspNetCore.Mvc;
using UdlaBlog.WebAPI.Local.Domain.Interfaces;
using UdlaBlog.WebAPI.Local.Domain.Models;
using UdlaBlog.WebAPI.Local.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UdlaBlog.WebAPI.Local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _repository;

        public TagsController(ITagRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
        {
            var tags = await _repository.GetAllAsync();
            var tagsDto = tags.Select(t => ConvertToDto(t));
            return Ok(tagsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTag(Guid id)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDto(tag));
        }

        [HttpPost]
        public async Task<ActionResult<TagDto>> PostTag(TagDto tagDto)
        {
            var tag = ConvertToEntity(tagDto);
            await _repository.AddAsync(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, ConvertToDto(tag));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(Guid id, TagDto tagDto)
        {
            if (id != tagDto.Id)
            {
                return BadRequest();
            }

            var tag = ConvertToEntity(tagDto);
            try
            {
                await _repository.UpdateAsync(tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _repository.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> TagExists(Guid id)
        {
            var tag = await _repository.GetByIdAsync(id);
            return tag != null;
        }

        private static TagDto ConvertToDto(Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Nombre = tag.Nombre,
                DisplayNombre = tag.DisplayNombre
            };
        }

        private static Tag ConvertToEntity(TagDto tagDto)
        {
            return new Tag
            {
                Id = tagDto.Id,
                Nombre = tagDto.Nombre,
                DisplayNombre = tagDto.DisplayNombre
            };
        }
    }
}
