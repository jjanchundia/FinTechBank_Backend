using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using C = FinTechBank.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cliente.Services.RemoteInterface;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class EditarCliente
    {
        public class EditarClienteCommand : IRequest<C.Result<ClienteDto>>
        {
            public int ClienteId { get; set; }
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string? NumeroCuenta { get; set; }
            public decimal? Saldo { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string? Direccion { get; set; }
            public string? Telefono { get; set; }
            public string Correo { get; set; }
            public string TipoCliente { get; set; }
            public string EstadoCivil { get; set; }
            public string NumeroIdentificacion { get; set; }
            public string ProfesionOcupacion { get; set; }
            public string Genero { get; set; }
            public string Nacionalidad { get; set; }
            public int UsuarioId { get; set; }
        }

        public class Handler : IRequestHandler<EditarClienteCommand, C.Result<ClienteDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IUsuarioService _usuarioService;
            public Handler(ApplicationDbContext dbcontext, IUsuarioService usuarioService)
            {
                _dbcontext = dbcontext;
                _usuarioService = usuarioService;
            }

            public async Task<C.Result<ClienteDto>> Handle(EditarClienteCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _usuarioService.GetUsuario(request.UsuarioId);

                if (usuario.usuario == null)
                {
                    return C.Result<ClienteDto>.Failure("No se encontró usuario!");
                }

                var cliente = await _dbcontext.Cliente.Where(x => x.ClienteId == request.ClienteId).FirstOrDefaultAsync();
                if (cliente == null)
                {
                    return C.Result<ClienteDto>.Failure("No se encontró cliente para actualizar!");
                }

                // Actualizar los campos del cliente con los valores proporcionados en la solicitud
                cliente.Nombre = request.Nombre;
                cliente.Apellido = request.Apellido;
                cliente.NumeroCuenta = request.NumeroCuenta;
                cliente.Saldo = request.Saldo;
                cliente.FechaNacimiento = request.FechaNacimiento;
                cliente.Direccion = request.Direccion;
                cliente.Telefono = request.Telefono;
                cliente.Correo = request.Correo;
                cliente.TipoCliente = request.TipoCliente;
                cliente.EstadoCivil = request.EstadoCivil;
                cliente.NumeroIdentificacion = request.NumeroIdentificacion;
                cliente.ProfesionOcupacion = request.ProfesionOcupacion;
                cliente.Genero = request.Genero;
                cliente.Nacionalidad = request.Nacionalidad;
                cliente.UsuarioId = request.UsuarioId;

                // Guardar los cambios en la base de datos
                await _dbcontext.SaveChangesAsync();

                return C.Result<ClienteDto>.Success(new ClienteDto
                {
                    ClienteId = request.ClienteId,
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    NumeroCuenta = request.NumeroCuenta,
                    Saldo = request.Saldo,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion,
                    Telefono = request.Telefono,
                    Correo = request.Correo,
                    TipoCliente = request.TipoCliente,
                    EstadoCivil = request.EstadoCivil,
                    NumeroIdentificacion = request.NumeroIdentificacion,
                    ProfesionOcupacion = request.ProfesionOcupacion,
                    Genero = request.Genero,
                    Nacionalidad = request.Nacionalidad,
                    UsuarioId = request.UsuarioId,
                    Nombres = usuario.usuario.Nombres,
                    Apellidos = usuario.usuario.Apellidos,
                    Username = usuario.usuario.Username,
                    Password = usuario.usuario.Password,
                    Role = usuario.usuario.Role
                });
            }
        }
    }
}
