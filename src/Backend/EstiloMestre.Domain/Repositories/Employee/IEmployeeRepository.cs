namespace EstiloMestre.Domain.Repositories.Employee;

public interface IEmployeeRepository
{
    Task Add(Entities.Employee employee);

    Task<HashSet<long>> GetRegisteredBarbershopServicesByEmployeeId(long employeeId);
    
    Task<Entities.Employee?> GetEmployeeById(long userId);
    Task<bool> ExistEmployeeById(long employeeId);
    Task<Entities.Employee?> GetEmployeeByUserId(long userId);
}
