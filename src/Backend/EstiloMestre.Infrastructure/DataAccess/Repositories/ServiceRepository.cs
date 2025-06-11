using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Service;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class ServiceRepository(EstiloMestreDbContext dbContext) : IServiceRepository
{
    public async Task Add(Service service)
    {
        await dbContext.Services.AddAsync(service);
    }

    public async Task<bool> ExistGlobalServiceByName(string name)
    {
        return await dbContext.Services.AsNoTracking().AnyAsync(s => s.Name.Equals(name));
    }

    public async Task<IList<Service>> GetAllGlobalServices()
    {
        return await dbContext.Services.AsNoTracking().ToListAsync();
    }

    public async Task<HashSet<long>> GetGlobalServicesIds()
    {
        return await dbContext.Services.AsNoTracking().Select(s => s.Id).ToHashSetAsync();
    }
}