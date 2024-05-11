using FinTechBank.Domain;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Cliente { get; set; }
    }
}