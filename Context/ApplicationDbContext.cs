using Microsoft.EntityFrameworkCore;
using vet_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace vet_backend.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Mascota> Mascotas { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Raza> Razas { get; set; }

    }

}
