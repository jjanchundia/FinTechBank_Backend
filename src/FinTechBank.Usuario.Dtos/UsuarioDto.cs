
namespace FinTechBank.Usuario.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Rol del usuario para autorización
        public string Token { get; set; }
    }
}