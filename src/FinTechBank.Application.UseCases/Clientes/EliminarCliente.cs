using FinTechBank.Persistence;
using FinTechBank.Domain;
using MediatR;

namespace FinTechBank.Application.UseCases.Clientes
{
    public class EliminarCliente
    {
        public class EliminarClienteCommand : IRequest<Result<string>>
        {
            public int ClienteId { get; set; }
        }

        public class Handler : IRequestHandler<EliminarClienteCommand, Result<string>>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<string>> Handle(EliminarClienteCommand request, CancellationToken cancellationToken)
            {
                var cliente = await _dbcontext.Cliente.FindAsync(request.ClienteId);

                if (cliente == null)
                {
                    // El cliente no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                    return Result<string>.Failure("No se encontró cliente para eliminar!");
                }

                // 2. Eliminar el cliente
                _dbcontext.Cliente.Remove(cliente);

                // 3. Guardar los cambios en la base de datos
                await _dbcontext.SaveChangesAsync();

                return Result<string>.Success("Cliente eliminado correctamente!");
            }
        }
    }
}