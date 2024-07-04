using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Application.DTOs;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;

[Route("api/nodo/blogs")]
[ApiController]
public class BlogNodoController : ControllerBase
{
    private readonly IBlogNodoRepository _blogRepository;
    private readonly ITagRepository _tagRepository;

    public BlogNodoController(IBlogNodoRepository blogRepository, ITagRepository tagRepository)
    {
        _blogRepository = blogRepository;
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogNodo>>> GetBlogs()
    {
        var blogs = await _blogRepository.GetAllAsync();
        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogNodo>> GetBlog(Guid id)
    {
        var blog = await _blogRepository.GetByIdAsync(id);
        if (blog == null)
        {
            return NotFound();
        }
        return Ok(blog);
    }

    [HttpPost]
    public async Task<ActionResult> PostBlog(BlogNodoDto blogNodoDto)
    {
        var tags = new List<Tag>();
        foreach (var tagDto in blogNodoDto.Tags)
        {
            var tag = await _tagRepository.GetByIdAsync(tagDto.Id);
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        var blogNodo = new BlogNodo
        {
            Encabezado = blogNodoDto.Encabezado,
            TituloPagina = blogNodoDto.TituloPagina,
            Contenido = blogNodoDto.Contenido,
            DescripcionCorta = blogNodoDto.DescripcionCorta,
            UrlImagenDestacada = blogNodoDto.UrlImagenDestacada,
            FechaPublicacion = blogNodoDto.FechaPublicacion,
            Autor = blogNodoDto.Autor,
            Visible = blogNodoDto.Visible,
            Tags = tags
        };

        await _blogRepository.AddAsync(blogNodo);
        return CreatedAtAction(nameof(GetBlog), new { id = blogNodo.Id }, blogNodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(Guid id, BlogNodoDto blogNodoDto)
    {
        if (id != blogNodoDto.Id)
        {
            return BadRequest();
        }

        var tags = new List<Tag>();
        foreach (var tagDto in blogNodoDto.Tags)
        {
            var tag = await _tagRepository.GetByIdAsync(tagDto.Id);
            if (tag != null)
            {
                tags.Add(tag);
            }
        }

        var blogNodo = new BlogNodo
        {
            Id = blogNodoDto.Id,
            Encabezado = blogNodoDto.Encabezado,
            TituloPagina = blogNodoDto.TituloPagina,
            Contenido = blogNodoDto.Contenido,
            DescripcionCorta = blogNodoDto.DescripcionCorta,
            UrlImagenDestacada = blogNodoDto.UrlImagenDestacada,
            FechaPublicacion = blogNodoDto.FechaPublicacion,
            Autor = blogNodoDto.Autor,
            Visible = blogNodoDto.Visible,
            Tags = tags
        };

        await _blogRepository.UpdateAsync(blogNodo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _blogRepository.DeleteAsync(id);
        return NoContent();
    }
}
