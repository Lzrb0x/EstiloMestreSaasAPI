namespace EstiloMestre.Domain.Repositories.BarbershopService;

public interface IBarbershopServiceRepository
{
    Task<HashSet<long>> GetBarbershopServicesIdsByBarbershopId(long barbershopId);
    Task AddRange(List<Entities.BarbershopService> barbershopServices);
    Task Add(Entities.BarbershopService barbershopService);
}
