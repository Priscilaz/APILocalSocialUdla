using System;

namespace UdlaBlog.WebAPI.Local.Domain.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroTelefono { get; set; }
    }
}
