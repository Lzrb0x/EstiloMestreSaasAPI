namespace EstiloMestre.Domain.Repositories.Service;

public interface IServiceRepository
{
    Task Add(Entities.Service service);
    
    Task<bool> ExistGlobalServiceByName(string name);

    Task<IList<Entities.Service>> GetAllGlobalServices();

    Task<HashSet<long>> GetGlobalServicesIds();
}
