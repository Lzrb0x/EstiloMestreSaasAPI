using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.BarbershopService;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopServiceRepository(EstiloMestreDbContext dbContext) : IBarbershopServiceRepository
{
    public async Task AddRange(List<BarbershopService> barbershopServices)
    {
        await dbContext.BarbershopServices.AddRangeAsync(barbershopServices);
    }

    public async Task Add(BarbershopService barbershopService)
    {
        await dbContext.BarbershopServices.AddAsync(barbershopService);
    }

    public async Task<BarbershopService?> GetById(long barbershopServiceId)
    {
        return await dbContext.BarbershopServices
            .AsNoTracking()
            .FirstOrDefaultAsync(bs => bs.Id == barbershopServiceId && bs.Active);
    }

    public async Task<HashSet<long>> GetGlobalServicesAlreadyRegisteredOnBarbershop(long barbershopId)
    {
        return await dbContext.BarbershopServices.AsNoTracking()
            .Where(bs => bs.BarbershopId == barbershopId && bs.Active)
            .Select(bs => bs.ServiceId)
            .ToHashSetAsync();
    }

    public async Task<HashSet<long>> GetBarbershopServicesIds(long barbershopId)
    {
        return await dbContext.BarbershopServices.AsNoTracking()
            .Where(bs => bs.BarbershopId == barbershopId)
            .Select(bs => bs.Id)
            .ToHashSetAsync();
    }

    public async Task<List<BarbershopService>> GetBarbershopServicesByBarbershop(long barbershopId)
    {
        return await dbContext.BarbershopServices
            .AsNoTracking()
            .Where(bs => bs.BarbershopId == barbershopId)
            .ToListAsync();
    }
}