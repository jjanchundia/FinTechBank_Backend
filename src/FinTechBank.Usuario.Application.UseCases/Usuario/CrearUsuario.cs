using FinTechBank.Usuario.Persistence;
using U = FinTechBank.Usuario.Domain;
using FinTechBank.Usuario.Dtos;
using MediatR;

namespace FinTechBank.Usuario.Application.UseCases.Usuario
{
    public class CrearUsuario
    {
        public class CrearUsuarioCommand : IRequest<U.Result<UsuarioDto>>
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class Handler : IRequestHandler<CrearUsuarioCommand, U.Result<UsuarioDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<U.Result<UsuarioDto>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
            {
                var nuevo = new U.Usuario()
                {
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Username = request.Username,
                    Password = request.Password,
                    Role = request.Role
                };

                await _dbcontext.Usuario.AddAsync(nuevo);
                await _dbcontext.SaveChangesAsync();

                var ultimoIdInsertado = nuevo.Id;

                return U.Result<UsuarioDto>.Success(new UsuarioDto
                {
                    Id = ultimoIdInsertado,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Username = request.Username,
                    Password = request.Password,
                    Role = request.Role
                });
            }
        }
    }
}