using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Application.DTOs;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;

[Route("api/fica/blogs")]
[ApiController]
public class BlogFicaController : ControllerBase
{
    private readonly IBlogFicaRepository _blogRepository;
    private readonly ITagRepository _tagRepository;

    public BlogFicaController(IBlogFicaRepository blogRepository, ITagRepository tagRepository)
    {
        _blogRepository = blogRepository;
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogFica>>> GetBlogs()
    {
        var blogs = await _blogRepository.GetAllAsync();
        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogFica>> GetBlog(Guid id)
    {
        var blog = await _blogRepository.GetByIdAsync(id);
        if (blog == null)
        {
            return NotFound();
        }
        return Ok(blog);
    }

    [HttpPost]
    public async Task<ActionResult> PostBlog(BlogFicaDto blogFicaDto)
    {
        var tags = new List<Tag>();
        foreach (var tagDto in blogFicaDto.Tags)
        {
            var tag = await _tagRepository.GetByIdAsync(tagDto.Id);
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        var blogFica = new BlogFica
        {
            Encabezado = blogFicaDto.Encabezado,
            TituloPagina = blogFicaDto.TituloPagina,
            Contenido = blogFicaDto.Contenido,
            DescripcionCorta = blogFicaDto.DescripcionCorta,
            UrlImagenDestacada = blogFicaDto.UrlImagenDestacada,
            FechaPublicacion = blogFicaDto.FechaPublicacion,
            Autor = blogFicaDto.Autor,
            Visible = blogFicaDto.Visible,
            Tags = tags
        };

        await _blogRepository.AddAsync(blogFica);
        return CreatedAtAction(nameof(GetBlog), new { id = blogFica.Id }, blogFica);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(Guid id, BlogFicaDto blogFicaDto)
    {
        if (id != blogFicaDto.Id)
        {
            return BadRequest();
        }

        var tags = new List<Tag>();
        foreach (var tagDto in blogFicaDto.Tags)
        {
            var tag = await _tagRepository.GetByIdAsync(tagDto.Id);
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        var blogFica = new BlogFica
        {
            Id = blogFicaDto.Id,
            Encabezado = blogFicaDto.Encabezado,
            TituloPagina = blogFicaDto.TituloPagina,
            Contenido = blogFicaDto.Contenido,
            DescripcionCorta = blogFicaDto.DescripcionCorta,
            UrlImagenDestacada = blogFicaDto.UrlImagenDestacada,
            FechaPublicacion = blogFicaDto.FechaPublicacion,
            Autor = blogFicaDto.Autor,
            Visible = blogFicaDto.Visible,
            Tags = tags
        };

        await _blogRepository.UpdateAsync(blogFica);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _blogRepository.DeleteAsync(id);
        return NoContent();
    }
    //cambio
}
