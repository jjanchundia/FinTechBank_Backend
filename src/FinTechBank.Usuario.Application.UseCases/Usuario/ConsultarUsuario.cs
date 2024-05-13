using FinTechBank.Usuario.Domain;
using FinTechBank.Usuario.Dtos;
using FinTechBank.Usuario.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinTechBank.Usuario.Application.UseCases.Usuario
{
    public class ConsultarUsuario
    {
        public class ConsultarUsuarioCommand : IRequest<Result<UsuarioDto>>
        {
            public int UsuarioId { get; set; }
        }

        public class Handler : IRequestHandler<ConsultarUsuarioCommand, Result<UsuarioDto>>
        {
            private readonly ApplicationDbContext _dbcontext;

            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<UsuarioDto>> Handle(ConsultarUsuarioCommand request, CancellationToken cancellationToken)
            {
                var user = await _dbcontext.Usuario.Where(x => x.Id == request.UsuarioId).FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<UsuarioDto>.Failure("No se encontró usuario!");
                }

                var usuarioDto = new UsuarioDto
                {
                    Id = user.Id,
                    Nombres = user.Nombres,
                    Apellidos = user.Apellidos,
                    Username = user.Username,
                    Password = user.Password,
                    Role = user.Role
                };

                return Result<UsuarioDto>.Success(usuarioDto);
            }
        }
    }
}