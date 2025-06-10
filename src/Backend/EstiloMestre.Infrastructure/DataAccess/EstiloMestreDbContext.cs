using EstiloMestre.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess;

public class EstiloMestreDbContext(DbContextOptions<EstiloMestreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Barbershop> Barbershops { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<BarbershopService> BarbershopServices { get; set; }
    
    public DbSet<ServiceEmployee> ServiceEmployees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstiloMestreDbContext).Assembly);
    }
}
