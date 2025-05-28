namespace EstiloMestre.Domain.Repositories.Service;

public interface IServiceRepository
{
    Task Add(Entities.Service service);
    Task<bool> ExistServiceByName(string name);
}
