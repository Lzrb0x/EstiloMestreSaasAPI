using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.ServiceEmployee;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class ServiceEmployeeRepository(EstiloMestreDbContext dbContext) : IServiceEmployeeRepository
{
    public async Task Add(ServiceEmployee employeeService)
    {
        await dbContext.ServiceEmployees.AddAsync(employeeService);
    }
}
