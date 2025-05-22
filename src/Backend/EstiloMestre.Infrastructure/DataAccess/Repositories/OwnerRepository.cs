using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Owner;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly EstiloMestreDbContext _dbContext;

    public OwnerRepository(EstiloMestreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Owner?> GetByUserId(long userId)
    {
        return await _dbContext.Owners.FirstOrDefaultAsync(o => o.UserId == userId);
    }

    public async Task Add(Owner owner)
    {
        await _dbContext.Owners.AddAsync(owner);
    }
}
