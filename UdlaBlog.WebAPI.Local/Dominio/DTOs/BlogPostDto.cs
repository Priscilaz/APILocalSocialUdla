using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.WebAPI.Local.DTOs
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Encabezado { get; set; }

        [Required]
        public string TituloPagina { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public string DescripcionCorta { get; set; }

        public string UrlImagenDestacada { get; set; }

        public string ManejadorUrl { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public string Autor { get; set; }

        [Required]
        public bool Visible { get; set; }

        public IFormFile ImagenDestacada { get; set; } // Para subir la imagen

        public ICollection<CommentDto> Comments { get; set; } // Colección de comentarios
    }
}
