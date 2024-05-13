using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using FinTechBank.Domain;
using MediatR;
using Cliente.Services.RemoteInterface;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class AgregarCliente
    {
        public class AgregarClienteCommand : IRequest<Result<ClienteDto>>
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

        public class Handler : IRequestHandler<AgregarClienteCommand, Result<ClienteDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IUsuarioService _usuarioService;
            public Handler(ApplicationDbContext dbcontext, IUsuarioService usuarioService)
            {
                _dbcontext = dbcontext;
                _usuarioService = usuarioService;
            }

            public async Task<Result<ClienteDto>> Handle(AgregarClienteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var usuario = await _usuarioService.GetUsuario(request.UsuarioId);

                    DateTime dateTime = request.FechaNacimiento;
                    DateTime dateTimeUtc = dateTime.ToUniversalTime();

                    var nuevo = new Domain.Cliente()
                    {
                        ClienteId = request.ClienteId,
                        Nombre = request.Nombre,
                        Apellido = request.Apellido,
                        NumeroCuenta = request.NumeroCuenta,
                        Saldo = request.Saldo,
                        FechaNacimiento = dateTimeUtc,
                        Direccion = request.Direccion,
                        Telefono = request.Telefono,
                        Correo = request.Correo,
                        TipoCliente = request.TipoCliente,
                        EstadoCivil = request.EstadoCivil,
                        NumeroIdentificacion = request.NumeroIdentificacion,
                        ProfesionOcupacion = request.ProfesionOcupacion,
                        Genero = request.Genero,
                        Nacionalidad = request.Nacionalidad,
                        UsuarioId = request.UsuarioId
                    };

                    await _dbcontext.Cliente.AddAsync(nuevo);
                    await _dbcontext.SaveChangesAsync();

                    var ultimoIdInsertado = nuevo.ClienteId;

                    return Result<ClienteDto>.Success(new ClienteDto
                    {
                        ClienteId = ultimoIdInsertado,
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
                catch (Exception e)
                {
                    Console.WriteLine("Error", e.Message);
                    throw;
                }
            }
        }
    }
}