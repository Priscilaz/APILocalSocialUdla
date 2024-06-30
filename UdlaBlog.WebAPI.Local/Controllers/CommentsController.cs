using Microsoft.AspNetCore.Mvc;
using UdlaBlog.WebAPI.Local.Domain.Interfaces;
using UdlaBlog.WebAPI.Local.Domain.Models;
using UdlaBlog.WebAPI.Local.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UdlaBlog.WebAPI.Local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;

        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{blogPostId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(Guid blogPostId)
        {
            var comments = await _repository.GetAllByBlogPostIdAsync(blogPostId);
            var commentsDto = comments.Select(c => ConvertToDto(c));
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetComment(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDto(comment));
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> PostComment(CommentDto commentDto)
        {
            var comment = ConvertToEntity(commentDto);
            await _repository.AddAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, ConvertToDto(comment));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(Guid id, CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }

            var comment = ConvertToEntity(commentDto);
            try
            {
                await _repository.UpdateAsync(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CommentExists(id))
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
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CommentExists(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return comment != null;
        }

        private static CommentDto ConvertToDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Contenido = comment.Contenido,
                Fecha = comment.Fecha,
                BlogPostId = comment.BlogPostId
            };
        }

        private static Comment ConvertToEntity(CommentDto commentDto)
        {
            return new Comment
            {
                Id = commentDto.Id,
                Contenido = commentDto.Contenido,
                Fecha = commentDto.Fecha,
                BlogPostId = commentDto.BlogPostId
            };
        }
    }
}
