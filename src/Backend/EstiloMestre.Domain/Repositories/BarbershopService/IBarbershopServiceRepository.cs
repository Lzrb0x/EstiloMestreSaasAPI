namespace EstiloMestre.Domain.Repositories.BarbershopService;

public interface IBarbershopServiceRepository
{
    Task AddRange(List<Entities.BarbershopService> barbershopServices);

    Task Add(Entities.BarbershopService barbershopService);

    Task<HashSet<long>> GetGlobalServicesAlreadyRegisteredOnBarbershop(long barbershopId);

    Task<HashSet<long>> GetBarbershopServicesIds(long barbershopId);
}
