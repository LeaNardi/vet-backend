using Microsoft.EntityFrameworkCore;
using vet_backend.Models;

namespace vet_backend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Mascota> Mascotas { get; set; }

        public DbSet<User> Users { get; set; }
    }

}
