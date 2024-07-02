using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlaBlog.WebAPI.Local.Domain.Interfaces;
using UdlaBlog.WebAPI.Local.DTOs;
using UdlaBlog.WebAPI.Local.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace UdlaBlog.WebAPI.Local.Controllers
{
    [Route("api/nodo/blogs")]
    [ApiController]
    public class NodoBlogsController : ControllerBase
    {
        private readonly IBlogPostRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NodoBlogsController(IBlogPostRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetNodoBlogs()
        {
            var blogPosts = await _repository.GetBySectionAsync("Nodo");
            var blogPostsDto = blogPosts.Select(b => ConvertToDto(b));
            return Ok(blogPostsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetNodoBlog(Guid id)
        {
            var blogPost = await _repository.GetByIdAndSectionAsync(id, "Nodo");
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDto(blogPost));
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> PostNodoBlog([FromForm] BlogPostDto blogPostDto)
        {
            var blogPost = ConvertToEntity(blogPostDto);
            blogPost.Section = "Nodo";

            if (blogPostDto.ImagenDestacada != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", blogPostDto.ImagenDestacada.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await blogPostDto.ImagenDestacada.CopyToAsync(stream);
                }
                blogPost.RutaImagen = $"/uploads/{blogPostDto.ImagenDestacada.FileName}";
            }

            await _repository.AddAsync(blogPost);
            return CreatedAtAction(nameof(GetNodoBlog), new { id = blogPost.Id }, ConvertToDto(blogPost));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNodoBlog(Guid id, [FromForm] BlogPostDto blogPostDto)
        {
            if (id != blogPostDto.Id)
            {
                return BadRequest();
            }

            var blogPost = ConvertToEntity(blogPostDto);
            blogPost.Section = "Nodo";

            if (blogPostDto.ImagenDestacada != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", blogPostDto.ImagenDestacada.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await blogPostDto.ImagenDestacada.CopyToAsync(stream);
                }
                blogPost.RutaImagen = $"/uploads/{blogPostDto.ImagenDestacada.FileName}";
            }

            try
            {
                await _repository.UpdateAsync(blogPost);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BlogPostExists(id))
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
        public async Task<IActionResult> DeleteNodoBlog(Guid id)
        {
            var blogPost = await _repository.GetByIdAndSectionAsync(id, "Nodo");
            if (blogPost == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> BlogPostExists(Guid id)
        {
            var blogPost = await _repository.GetByIdAsync(id);
            return blogPost != null;
        }

        private static BlogPostDto ConvertToDto(BlogPost blogPost)
        {
            return new BlogPostDto
            {
                Id = blogPost.Id,
                Encabezado = blogPost.Encabezado,
                TituloPagina = blogPost.TituloPagina,
                Contenido = blogPost.Contenido,
                DescripcionCorta = blogPost.DescripcionCorta,
                UrlImagenDestacada = blogPost.UrlImagenDestacada,
                ManejadorUrl = blogPost.ManejadorUrl,
                FechaPublicacion = blogPost.FechaPublicacion,
                Autor = blogPost.Autor,
                Visible = blogPost.Visible,
                ImagenDestacada = null,
                Comments = blogPost.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    Fecha = c.Fecha,
                    BlogPostId = c.BlogPostId
                }).ToList(),
                Section = blogPost.Section
            };
        }

        private static BlogPost ConvertToEntity(BlogPostDto blogPostDto)
        {
            return new BlogPost
            {
                Id = blogPostDto.Id,
                Encabezado = blogPostDto.Encabezado,
                TituloPagina = blogPostDto.TituloPagina,
                Contenido = blogPostDto.Contenido,
                DescripcionCorta = blogPostDto.DescripcionCorta,
                UrlImagenDestacada = blogPostDto.UrlImagenDestacada,
                ManejadorUrl = blogPostDto.ManejadorUrl,
                FechaPublicacion = blogPostDto.FechaPublicacion,
                Autor = blogPostDto.Autor,
                Visible = blogPostDto.Visible,
                RutaImagen = blogPostDto.ImagenDestacada?.FileName,
                Section = blogPostDto.Section
            };
        }
    }
}
