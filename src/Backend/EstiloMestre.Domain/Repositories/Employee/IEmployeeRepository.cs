namespace EstiloMestre.Domain.Repositories.Employee;

public interface IEmployeeRepository
{
    Task Add(Entities.Employee employee);
    Task<bool> ExistRegisteredEmployeeWithUserId(long userId);
    
    Task<bool> ExistRegisteredEmployeeWithUserIdAndBarbershopId(long userId, long barbershopId);

    Task<HashSet<long>> GetRegisteredBarbershopServicesByEmployeeId(long employeeId);
    
    Task<Entities.Employee?> GetEmployeeById(long userId);
}
