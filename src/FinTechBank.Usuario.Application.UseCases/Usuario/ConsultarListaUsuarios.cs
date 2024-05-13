using FinTechBank.Usuario.Domain;
using FinTechBank.Usuario.Dtos;
using FinTechBank.Usuario.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinTechBank.Usuario.Application.UseCases.Usuario
{
    public class ConsultarListaUsuarios
    {
        public class ConsultarUsuarioCommand : IRequest<Result<List<UsuarioDto>>>
        {
            public int UsuarioId { get; set; }
        }

        public class Handler : IRequestHandler<ConsultarUsuarioCommand, Result<List<UsuarioDto>>>
        {
            private readonly ApplicationDbContext _dbcontext;

            public Handler(ApplicationDbContext dbcontext)
            {
                _dbcontext = dbcontext;
            }

            public async Task<Result<List<UsuarioDto>>> Handle(ConsultarUsuarioCommand request, CancellationToken cancellationToken)
            {
                var users = await _dbcontext.Usuario.ToListAsync();
                var usersDto = users.Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                }).ToList();
                return Result<List<UsuarioDto>>.Success(usersDto);
            }
        }
    }
}