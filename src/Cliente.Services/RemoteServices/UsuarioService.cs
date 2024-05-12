using Cliente.Services.RemoteInterface;
using Cliente.Services.RemoteModel;
using FinTechBank.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cliente.Services.RemoteServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IHttpClientFactory httpClient, ILogger<UsuarioService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, UsuarioRemote usuario, string errorMessage)> GetUsuario(int usuarioId)
        {
            try
            {
                var user = _httpClient.CreateClient("Usuario");
                var response = await user.GetAsync($"api/Usuario/{usuarioId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Result<UsuarioRemote>>(content, options);
                    return (true, result.Value, "Ok");
                }

                return (false, null, "null");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}