using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class EmployeeRepository(EstiloMestreDbContext dbContext) : IEmployeeRepository
{
    public async Task Add(Employee employee)
    {
        await dbContext.Employees.AddAsync(employee);
    }

    public async Task<bool> ExistRegisteredEmployeeWithUserId(long userId)
    {
        return await dbContext.Employees.AsNoTracking().AnyAsync(e => e.UserId == userId);
    }
}
