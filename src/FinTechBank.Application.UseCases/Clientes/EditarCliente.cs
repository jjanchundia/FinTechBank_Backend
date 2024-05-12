using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using FinTechBank.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class EditarCliente
    {
        public class EditarClienteCommand : IRequest<Result<ClienteDto>>
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
        }

        public class Handler : IRequestHandler<EditarClienteCommand, Result<ClienteDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<ClienteDto>> Handle(EditarClienteCommand request, CancellationToken cancellationToken)
            {
                var cliente = await _dbcontext.Cliente.Where(x => x.ClienteId == request.ClienteId).FirstOrDefaultAsync();
                if (cliente == null)
                {
                    return Result<ClienteDto>.Failure("No se encontró cliente para actualizar!");
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

                // Guardar los cambios en la base de datos
                await _dbcontext.SaveChangesAsync();

                return Result<ClienteDto>.Success(new ClienteDto
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
                    Nacionalidad = request.Nacionalidad
                });
            }
        }
    }
}
