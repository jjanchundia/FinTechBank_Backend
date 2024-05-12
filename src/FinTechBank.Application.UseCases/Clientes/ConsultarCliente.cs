using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using FinTechBank.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cliente.Services.RemoteInterface;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class ConsultarCliente
    {
        public class ConsultarClienteRequest : IRequest<Result<List<ClienteDto>>>
        {
        }

        public class Handler : IRequestHandler<ConsultarClienteRequest, Result<List<ClienteDto>>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IUsuarioService _usuarioService;

            public Handler(ApplicationDbContext dbcontext, IUsuarioService usuarioService)
            {
                _dbcontext = dbcontext;
                _usuarioService = usuarioService;
            }

            public async Task<Result<List<ClienteDto>>> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
            {
                var clientes = await _dbcontext.Cliente.ToListAsync();
                var clientesDto = new List<ClienteDto>();

                foreach (var item in clientes)
                {
                    var usuario = await _usuarioService.GetUsuario(item.UsuarioId);

                    var clienteDto = new ClienteDto
                    {
                        ClienteId = item.ClienteId,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        NumeroCuenta = item.NumeroCuenta,
                        Saldo = item.Saldo,
                        FechaNacimiento = item.FechaNacimiento,
                        Direccion = item.Direccion,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        TipoCliente = item.TipoCliente,
                        EstadoCivil = item.EstadoCivil,
                        NumeroIdentificacion = item.NumeroIdentificacion,
                        ProfesionOcupacion = item.ProfesionOcupacion,
                        Genero = item.Genero,
                        Nacionalidad = item.Nacionalidad,
                        //Datos del usuario que vienen del otro microservicio
                        UsuarioId = usuario.usuario.Id,
                        Nombres = usuario.usuario.Nombres,
                        Apellidos = usuario.usuario.Apellidos,
                        Username = usuario.usuario.Username,
                        Password = usuario.usuario.Password,
                        Role = usuario.usuario.Role
                    };

                    clientesDto.Add(clienteDto);
                }
                return Result<List<ClienteDto>>.Success(clientesDto);
            }
        }
    }
}