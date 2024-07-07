using System.ComponentModel.DataAnnotations;

namespace UdlaBlog.Domain.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string CorreoElectronico { get; set; }
    }
}
