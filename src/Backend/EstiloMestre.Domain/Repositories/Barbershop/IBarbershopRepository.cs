namespace EstiloMestre.Domain.Repositories.Barbershop;

public interface IBarbershopRepository
{
    Task Add(Entities.Barbershop barbershop);
    
    Task<bool> UserIsBarbershopOwner(long ownerId, long barbershopId);
}
