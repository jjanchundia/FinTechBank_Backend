using FinTechBank.Usuario.Domain;
using FinTechBank.Usuario.Dtos;
using FinTechBank.Usuario.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinTechBank.Usuario.Application.UseCases.Usuario
{
    public class EditarUsuario
    {
        public class EditarUsuarioCommand : IRequest<Result<UsuarioDto>>
        {
            public int Id { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class Handler : IRequestHandler<EditarUsuarioCommand, Result<UsuarioDto>>
        {
            private readonly ApplicationDbContext _dbcontext;
            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<UsuarioDto>> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var usuario = await _dbcontext.Usuario.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                    if (usuario == null)
                    {
                        return Result<UsuarioDto>.Failure("No se encontró usuario para actualizar!");
                    }

                    // Actualizar los campos del usuario con los valores proporcionados en la solicitud
                    usuario.Nombres = request.Nombres;
                    usuario.Apellidos = request.Apellidos;
                    usuario.Username = request.Username;
                    usuario.Password = request.Password;
                    usuario.Role = request.Role;

                    // Guardar los cambios en la base de datos
                    await _dbcontext.SaveChangesAsync();

                    return Result<UsuarioDto>.Success(new UsuarioDto
                    {
                        Nombres = request.Nombres,
                        Apellidos = request.Apellidos,
                        Username = request.Username,
                        Password = request.Password,
                        Role = request.Role
                    });
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync(e.Message);
                    return Result<UsuarioDto>.Failure($"Error interno:! {e.Message}");
                }
            }
        }
    }
}