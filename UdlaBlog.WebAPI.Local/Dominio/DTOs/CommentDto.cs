using System;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.WebAPI.Local.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Guid BlogPostId { get; set; }
    }
}
