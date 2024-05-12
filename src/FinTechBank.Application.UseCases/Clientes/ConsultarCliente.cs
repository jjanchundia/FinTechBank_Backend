using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using FinTechBank.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<List<ClienteDto>>> Handle(ConsultarClienteRequest request, CancellationToken cancellationToken)
            {
                var clientes = await _dbcontext.Cliente.ToListAsync();
                var clientesDto = clientes.Select(cliente => new ClienteDto
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
                }).ToList();

                return Result<List<ClienteDto>>.Success(clientesDto);
            }
        }
    }
}