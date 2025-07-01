using EstiloMestre.Communication.DTOs;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;

namespace EstiloMestre.Application.Services.GetEmployeeWorkingBlocks;

public class GetEmployeeWorkingBlocks(
    IWorkingHourOverrideRepository overrideHourRepository,
    IWorkingHourRepository workingHourRepository)
{
    public async Task<IList<TimeBlock>> GetWorkingBlocks(long employeeId, DateOnly date)
    {
        var overrides = await overrideHourRepository.GetByEmployeeIdAndDate(employeeId, date);

        if (overrides.Any())
        {
            return overrides.Any(o => o.IsDayOff)
                ? [new TimeBlock(default, default, IsDayOff: true)]
                : overrides.Select(o => new TimeBlock(o.StartTime, o.EndTime)).ToList();
        }

        var standartHours = await workingHourRepository.GetByEmployeeIdAndDay(employeeId, date.DayOfWeek);

        if (!standartHours.Any() || standartHours.Any(x => x.IsDayOff))
        {
            return [new TimeBlock(default, default, IsDayOff: true)];
        }
        
        return standartHours.Select(x => new TimeBlock(x.StartTime, x.EndTime)).ToList();
    }
}