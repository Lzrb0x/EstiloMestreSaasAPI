using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Barbershop;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopRepository(EstiloMestreDbContext dbContext) : IBarbershopRepository
{
    public async Task Add(Barbershop barbershop)
    {
        await dbContext.Barbershops.AddAsync(barbershop);
    }
}
