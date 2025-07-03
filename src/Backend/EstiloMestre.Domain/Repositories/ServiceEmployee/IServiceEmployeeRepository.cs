namespace EstiloMestre.Domain.Repositories.ServiceEmployee;

public interface IServiceEmployeeRepository
{
    Task Add(Entities.ServiceEmployee employeeService);

    Task<bool> EmployeePerformsService(long employeeId, long barbershopServiceId);
}
