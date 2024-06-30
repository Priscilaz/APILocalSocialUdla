using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.WebAPI.Local.Domain.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string DisplayNombre { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
