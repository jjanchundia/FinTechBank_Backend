using U = FinTechBank.Usuario.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinTechBank.Usuario.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<U.Usuario> Usuario { get; set; }
    }
}