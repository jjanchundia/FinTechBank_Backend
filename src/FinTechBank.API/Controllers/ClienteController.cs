﻿using FinTechBank.Application.UseCases.Clientes;
using FinTechBank.Usuario.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinTechBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        //Uso de Mediatr que implementa el patrón Mediator en C#.
        //El patrón Mediator es un patrón de diseño de comportamiento que reduce el acoplamiento entre los componentes de un programa
        private readonly IMediator _mediator;
        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<IActionResult> ConsultarCLientes()
        {
            var response = await _mediator.Send(new ConsultarCliente.ConsultarClienteRequest());
            return Ok(response);
        }

        [HttpGet("{clienteId:int}")]
        public async Task<IActionResult> ConsultarClientePorId(int clienteId)
        {
            var response = await _mediator.Send(new ConsultarClientePorId.ConsultarClientePorIdCommand() { ClienteId = clienteId });
            return Ok(response);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateCliente(AgregarCliente.AgregarClienteCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> EditCliente(EditarCliente.EditarClienteCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("eliminar/{ClienteId:int}")]
        public async Task<IActionResult> DeleteCliente(int ClienteId)
        {
            var response = await _mediator.Send(new EliminarCliente.EliminarClienteCommand() { ClienteId = ClienteId });
            return Ok(response);
        }
    }
}
