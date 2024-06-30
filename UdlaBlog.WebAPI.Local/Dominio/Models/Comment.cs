using System;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.WebAPI.Local.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Guid BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}
