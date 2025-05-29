using EstiloMestre.Domain.DTOs;

namespace EstiloMestre.Domain.Repositories.Barbershop;

public interface IBarbershopRepository
{
    Task Add(Entities.Barbershop barbershop);
}
