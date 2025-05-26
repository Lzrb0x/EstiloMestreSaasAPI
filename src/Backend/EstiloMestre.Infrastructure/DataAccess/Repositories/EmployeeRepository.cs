using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Employee;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EstiloMestreDbContext _dbContext;

    public EmployeeRepository(EstiloMestreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
    }

    public async Task<Employee?> GetByUserId(long userId)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.UserId == userId);
    }
}
