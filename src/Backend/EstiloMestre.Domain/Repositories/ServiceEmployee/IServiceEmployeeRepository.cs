namespace EstiloMestre.Domain.Repositories.ServiceEmployee;

public interface IServiceEmployeeRepository
{
    Task Add(Entities.ServiceEmployee employeeService);
}
