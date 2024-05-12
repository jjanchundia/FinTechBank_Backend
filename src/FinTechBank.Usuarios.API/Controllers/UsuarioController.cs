using FinTechBank.Usuario.Application.UseCases.Usuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinTechBank.Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public UsuarioController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUsuario(CrearUsuario.CrearUsuarioCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginCommand command)
        //{
        //    //Se retorna un UserDto con los datos del usuario logueado mas el Token
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}
    }
}