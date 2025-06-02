using EstiloMestre.Domain.DTOs;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.BarbershopService;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopServiceRepository(EstiloMestreDbContext dbContext) : IBarbershopServiceRepository
{
    public async Task<HashSet<long>> GetBarbershopServicesIdsByBarbershopId(long barbershopId)
    {
        return await dbContext.BarbershopServices.AsNoTracking()
           .Where(bs => bs.BarbershopId == barbershopId)
           .Select(bs => bs.ServiceId)
           .ToHashSetAsync();
    }
    
    public async Task AddRange(List<BarbershopService> barbershopServices)
    {
        await dbContext.BarbershopServices.AddRangeAsync(barbershopServices);
    }
}
