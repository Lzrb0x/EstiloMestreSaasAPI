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
        return await dbContext.Employees.AsNoTracking().AnyAsync(e => e.UserId == userId && e.Active == true);
    }

    public Task<bool> ExistRegisteredEmployeeWithUserIdAndBarbershopId(long userId, long barbershopId)
    {
        return dbContext.Employees.AsNoTracking()
           .AnyAsync(e => e.UserId == userId && e.Active == true && e.BarberShopId == barbershopId);
    }

    public async Task<HashSet<long>> GetRegisteredBarbershopServicesByEmployeeId(long employeeId)
    {
        return await dbContext.Employees.Include(e => e.ServicesEmployee)
           .Where(e => e.Id == employeeId && e.Active == true)
           .SelectMany(e => e.ServicesEmployee.Select(se => se.BarbershopServiceId))
           .ToHashSetAsync();
    }

    public async Task<Employee?> GetEmployeeById(long employeeId)
    {
        return await dbContext.Employees.AsNoTracking()
           .FirstOrDefaultAsync(e => e.Id == employeeId && e.Active == true);
    }
}
