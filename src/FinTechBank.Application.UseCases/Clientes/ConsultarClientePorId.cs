using Cliente.Services.RemoteInterface;
using FinTechBank.Application.Dtos;
using FinTechBank.Domain;
using FinTechBank.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class ConsultarClientePorId
    {
        public class ConsultarClientePorIdCommand : IRequest<Result<ClienteDto>>
        {
            public int ClienteId { get; set; }
        }

        public class Handler : IRequestHandler<ConsultarClientePorIdCommand, Result<ClienteDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IUsuarioService _usuarioService;

            public Handler(ApplicationDbContext dbcontext, IUsuarioService usuarioService)
            {
                _dbcontext = dbcontext;
                _usuarioService = usuarioService;
            }

            public async Task<Result<ClienteDto>> Handle(ConsultarClientePorIdCommand request, CancellationToken cancellationToken)
            {
                var cliente = await _dbcontext.Cliente.Where(x => x.ClienteId == request.ClienteId).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return Result<ClienteDto>.Failure("No se encontró usuario!");
                }

                var usuario = await _usuarioService.GetUsuario(cliente.UsuarioId);

                var librosDto = new ClienteDto
                {
                    ClienteId = cliente.ClienteId,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    NumeroCuenta = cliente.NumeroCuenta,
                    Saldo = cliente.Saldo,
                    FechaNacimiento = cliente.FechaNacimiento,
                    Direccion = cliente.Direccion,
                    Telefono = cliente.Telefono,
                    Correo = cliente.Correo,
                    TipoCliente = cliente.TipoCliente,
                    EstadoCivil = cliente.EstadoCivil,
                    NumeroIdentificacion = cliente.NumeroIdentificacion,
                    ProfesionOcupacion = cliente.ProfesionOcupacion,
                    Genero = cliente.Genero,
                    Nacionalidad = cliente.Nacionalidad,
                    //Datos del usuario que vienen del otro microservicio
                    UsuarioId = usuario.usuario.Id,
                    Nombres = usuario.usuario.Nombres,
                    Apellidos = usuario.usuario.Apellidos,
                    Username = usuario.usuario.Username,
                    Password = usuario.usuario.Password,
                    Role = usuario.usuario.Role
                };

                return Result<ClienteDto>.Success(librosDto);
            }
        }
    }
}