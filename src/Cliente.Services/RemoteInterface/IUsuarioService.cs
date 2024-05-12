
using Cliente.Services.RemoteModel;

namespace Cliente.Services.RemoteInterface
{
    public interface IUsuarioService
    {
        Task<(bool result, UsuarioRemote usuario, string errorMessage)> GetUsuario(int usuarioId);
    }
}