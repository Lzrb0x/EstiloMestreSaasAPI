using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Barbershop;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopRepository : IBarbershopRepository
{
    private readonly EstiloMestreDbContext _dbContext;

    public BarbershopRepository(EstiloMestreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Barbershop barbershop)
    {
        await _dbContext.Barbershops.AddAsync(barbershop);
    }

    public async Task<bool> UserIsBarbershopOwner(long ownerId, long barbershopId)
    {
        return await _dbContext.Barbershops.AnyAsync(b => b.Id == barbershopId && b.OwnerId == ownerId);
    }
}
