using System;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.Application.DTOs
{
    public class TagDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string DisplayNombre { get; set; }
    }
}
