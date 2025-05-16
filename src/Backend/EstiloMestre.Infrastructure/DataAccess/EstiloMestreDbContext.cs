using EstiloMestre.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess;

public class EstiloMestreDbContext : DbContext
{
    public EstiloMestreDbContext(DbContextOptions<EstiloMestreDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Barbershops> Barbershops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstiloMestreDbContext).Assembly);
    }
}