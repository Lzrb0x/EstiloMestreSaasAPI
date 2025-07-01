using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHourOverride.Set;

public class SetWorkingHourOverrideUseCase(
    IEmployeeRepository employeeRepository,
    IMapper mapper,
    IWorkingHourOverrideRepository workingHourOverrideRepository,
    IUnitOfWork unitOfWork)
    : ISetWorkingHourOverrideUseCase
{
    public async Task Execute(RequestWorkingHourOverrideJson request, long employeeId)
    {
        ValidateRequest(request);

        var newWoerkingHourOverride = mapper.Map<EmployeeWorkingHourOverride>(request);

        var existingWorkingHourOverrideByEmployee = await workingHourOverrideRepository.GetByEmployeeId(employeeId);

        ValidateOverride(newWoerkingHourOverride, existingWorkingHourOverrideByEmployee);

        newWoerkingHourOverride.EmployeeId = employeeId;

        await workingHourOverrideRepository.Add(newWoerkingHourOverride);
        await unitOfWork.Commit();
    }

    private static void ValidateOverride(EmployeeWorkingHourOverride newWorkingHourOverride,
        List<EmployeeWorkingHourOverride> existingWorkingHourOverrideByEmployee)
    {
        var overrideForSameDate =
            existingWorkingHourOverrideByEmployee.Where(x => x.Date == newWorkingHourOverride.Date).ToList();

        if (!overrideForSameDate.Any())
            return;

        var allOverridesForDate = new List<EmployeeWorkingHourOverride>(overrideForSameDate) { newWorkingHourOverride };

        var hasDayOff = allOverridesForDate.Any(o => o.IsDayOff);
        var hasWork = allOverridesForDate.Any(o => !o.IsDayOff);

        switch (hasDayOff)
        {
            case true when hasWork:
                throw new BusinessRuleException(ResourceMessagesExceptions.CANNOT_HAVE_DAY_OFF_WITH_WORKING_HOURS);
            case false:
            {
                var sortedHours = allOverridesForDate.OrderBy(o => o.StartTime).ToList();
                for (var i = 0; i < sortedHours.Count - 1; i++)
                {
                    if (sortedHours[i].EndTime > sortedHours[i + 1].StartTime)
                    {
                        throw new BusinessRuleException(ResourceMessagesExceptions.WORKING_HOURS_OVERLAP);
                    }
                }
                break;
            }
        }
    }


    private static void ValidateRequest(RequestWorkingHourOverrideJson request)
    {
        var validatorResult = new WorkingHourOverrideValidator().Validate(request);
        if (!validatorResult.IsValid)
            throw new OnValidationException(validatorResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
}