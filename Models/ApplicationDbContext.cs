using Microsoft.EntityFrameworkCore;

namespace vet_backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } 

        public DbSet<Mascota> Mascotas { get; set; }
    }

}
