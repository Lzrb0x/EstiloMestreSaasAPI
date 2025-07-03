using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.ServiceEmployee;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class ServiceEmployeeRepository(EstiloMestreDbContext dbContext) : IServiceEmployeeRepository
{
    public async Task Add(ServiceEmployee employeeService)
    {
        await dbContext.ServiceEmployees.AddAsync(employeeService);
    }

    public async Task<bool> EmployeePerformsService(long employeeId, long barbershopServiceId)
    {
        return await dbContext.ServiceEmployees.AnyAsync(e =>
            e.EmployeeId == employeeId && e.BarbershopServiceId == barbershopServiceId);
    }
}