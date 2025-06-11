using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Owner;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class OwnerRepository(EstiloMestreDbContext dbContext) : IOwnerRepository
{
    public async Task Add(Owner owner)
    {
        await dbContext.Owners.AddAsync(owner);
    }

    public async Task<Owner?> GetByUserId(long userId)
    {
        return await dbContext.Owners.FirstOrDefaultAsync(o => o.UserId == userId && o.Active);
    }
}