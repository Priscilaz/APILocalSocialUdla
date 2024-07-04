using System;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.Domain.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string DisplayNombre { get; set; }
    }
}
