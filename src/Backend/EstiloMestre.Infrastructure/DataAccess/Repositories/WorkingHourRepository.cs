using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class WorkingHourRepository(EstiloMestreDbContext dbContext) : IWorkingHourRepository
{
    public async Task AddRange(List<EmployeeWorkingHour> employeeWorkingHours)
    {
        await dbContext.EmployeeWorkingHours.AddRangeAsync(employeeWorkingHours);
    }

    public async Task<List<EmployeeWorkingHour>> GetByEmployeeId(long employeeId)
    {
        return await dbContext.EmployeeWorkingHours.Where(wh => wh.EmployeeId == employeeId).ToListAsync();
    }

    public async Task<IList<EmployeeWorkingHour>> GetByEmployeeIdAndDay(long employeeId, DayOfWeek dayOfWeek)
    {
        return await dbContext.EmployeeWorkingHours
            .Where(wh => wh.EmployeeId == employeeId && wh.DayOfWeek == dayOfWeek).ToListAsync();
    }
}