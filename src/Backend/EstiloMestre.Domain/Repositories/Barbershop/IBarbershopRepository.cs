
namespace EstiloMestre.Domain.Repositories.Barbershop;

public interface IBarbershopRepository
{
    Task Add(Entities.Barbershop barbershop);
    Task<IList<Entities.Barbershop>> GetForDashboard();
    Task<bool> UserIsBarbershopOwner(long userId, long barbershopId);
    
    Task<bool> UserIsOwnerOrEmployee(long userId, long barbershopId, long employeeId);
}
