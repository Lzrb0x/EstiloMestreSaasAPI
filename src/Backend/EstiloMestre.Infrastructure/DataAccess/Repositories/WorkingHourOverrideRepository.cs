using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class WorkingHourOverrideRepository(EstiloMestreDbContext dbContext) : IWorkingHourOverrideRepository
{
    public async Task Add(EmployeeWorkingHourOverride employeeWorkingHourOverride)
    {
        await dbContext.EmployeeWorkingHourOverrides.AddAsync(employeeWorkingHourOverride);
    }

    public async Task<List<EmployeeWorkingHourOverride>> GetByEmployeeId(long employeeId)
    {
        return await dbContext.EmployeeWorkingHourOverrides.AsNoTracking().Where(wh => wh.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<IList<EmployeeWorkingHourOverride>> GetByEmployeeIdAndDate(long employeeId, DateOnly date)
    {
        return await dbContext.EmployeeWorkingHourOverrides.Where(wh =>
            wh.EmployeeId == employeeId && wh.Date == date || wh.Date == date && wh.IsDayOff).ToListAsync();
    }
}