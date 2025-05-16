using EstiloMestre.Domain.Repositories;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstiloMestreDbContext _dbContext;
    public UnitOfWork(EstiloMestreDbContext context) => _dbContext = context;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}