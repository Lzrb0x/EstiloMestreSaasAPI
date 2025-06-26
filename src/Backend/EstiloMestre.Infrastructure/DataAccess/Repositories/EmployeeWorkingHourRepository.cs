using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class EmployeeWorkingHourRepository(EstiloMestreDbContext dbContext) : IEmployeeWorkingHourRepository
{
    public async Task AddRange(List<EmployeeWorkingHour> employeeWorkingHours)
    {
        await dbContext.EmployeeWorkingHours.AddRangeAsync(employeeWorkingHours);
    }

    public async Task<List<EmployeeWorkingHour>> GetByEmployeeId(long employeeId)
    {
        return await dbContext.EmployeeWorkingHours.Where(wh => wh.EmployeeId == employeeId).ToListAsync();
    }
}