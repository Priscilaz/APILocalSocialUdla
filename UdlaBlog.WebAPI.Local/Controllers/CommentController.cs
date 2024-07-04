using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdlaBlog.Domain.Entities;
using UdlaBlog.Domain.Interfaces;
using UdlaBlog.Application.DTOs;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet("byBlogFica/{blogFicaId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByBlogFicaId(Guid blogFicaId)
    {
        var comments = await _commentRepository.GetAllByBlogFicaIdAsync(blogFicaId);
        return Ok(comments);
    }

    [HttpGet("byBlogNodo/{blogNodoId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByBlogNodoId(Guid blogNodoId)
    {
        var comments = await _commentRepository.GetAllByBlogNodoIdAsync(blogNodoId);
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult> PostComment([FromBody] CommentDto commentDto)
    {
        var comment = new Comment
        {
            Contenido = commentDto.Contenido,
            Fecha = commentDto.Fecha,
            BlogFicaId = commentDto.BlogFicaId,
            BlogNodoId = commentDto.BlogNodoId
        };

        await _commentRepository.AddAsync(comment);
        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(Guid id, [FromBody] CommentDto commentDto)
    {
        if (id != commentDto.Id)
        {
            return BadRequest();
        }

        var comment = new Comment
        {
            Id = commentDto.Id,
            Contenido = commentDto.Contenido,
            Fecha = commentDto.Fecha,
            BlogFicaId = commentDto.BlogFicaId,
            BlogNodoId = commentDto.BlogNodoId
        };

        await _commentRepository.UpdateAsync(comment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        await _commentRepository.DeleteAsync(id);
        return NoContent();
    }
}
