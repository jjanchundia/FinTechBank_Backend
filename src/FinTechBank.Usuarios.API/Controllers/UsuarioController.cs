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

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarUsuarios()
        {
            var response = await _mediator.Send(new ConsultarListaUsuarios.ConsultarUsuarioCommand());
            return Ok(response);
        }

        [HttpGet("{usuarioId:int}")]
        public async Task<IActionResult> ConsultarUsuarioPorId(int usuarioId)
        {
            var response = await _mediator.Send(new ConsultarUsuario.ConsultarUsuarioCommand() { UsuarioId = usuarioId });
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUsuario(CrearUsuario.CrearUsuarioCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> EditUsuario(EditarUsuario.EditarUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login.LoginCommand command)
        {
            //Se retorna un UserDto con los datos del usuario logueado mas el Token
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}