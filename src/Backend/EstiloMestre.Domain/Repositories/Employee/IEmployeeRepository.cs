namespace EstiloMestre.Domain.Repositories.Employee;

public interface IEmployeeRepository
{
    Task Add(Entities.Employee employee);

    Task<Entities.Employee?> GetByUserId(long userId);
}
