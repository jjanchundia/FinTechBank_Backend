
namespace Cliente.Services.RemoteModel
{
    public class UsuarioRemote
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Rol del usuario para autorización
    }
}