using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Application.DTOs;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;

[Route("api/tags")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagRepository _tagRepository;

    public TagController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        var tags = await _tagRepository.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(Guid id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult> PostTag(TagDto tagDto)
    {
        var tag = new Tag
        {
            Nombre = tagDto.Nombre,
            DisplayNombre = tagDto.DisplayNombre
        };

        await _tagRepository.AddAsync(tag);
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTag(Guid id, TagDto tagDto)
    {
        if (id != tagDto.Id)
        {
            return BadRequest();
        }

        var tag = new Tag
        {
            Id = tagDto.Id,
            Nombre = tagDto.Nombre,
            DisplayNombre = tagDto.DisplayNombre
        };

        await _tagRepository.UpdateAsync(tag);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        await _tagRepository.DeleteAsync(id);
        return NoContent();
    }
}
