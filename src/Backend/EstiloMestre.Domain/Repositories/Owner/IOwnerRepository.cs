namespace EstiloMestre.Domain.Repositories.Owner;

public interface IOwnerRepository
{
    Task<Entities.Owner?> GetByUserId(long userId);
    Task Add(Entities.Owner owner);
}
