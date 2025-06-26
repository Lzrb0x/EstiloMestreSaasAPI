using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Set;

public class SetWorkingHourUseCase(
    IEmployeeRepository employeeRepository,
    IMapper mapper,
    IWorkingHourRepository workingHourRepository,
    IUnitOfWork unitOfWork) : ISetWorkingHourUseCase
{
    public async Task Execute(RequestEmployeeWorkingHourListJson request, long employeeId)
    {
        ValidateRequest(request);

        if (!await employeeRepository.ExistEmployeeById(employeeId))
            throw new NotFoundException(ResourceMessagesExceptions.EMPLOYEE_NOT_FOUND);

        var newWorkingHours = mapper.Map<List<EmployeeWorkingHour>>(request.WorkingHours);

        var existingWorkingHours = await workingHourRepository.GetByEmployeeId(employeeId);

        ValidateScheduleConflicts(newWorkingHours, existingWorkingHours);

        newWorkingHours.ForEach(wh => wh.EmployeeId = employeeId);
        
        await workingHourRepository.AddRange(newWorkingHours);
        await unitOfWork.Commit();
    }

    private static void ValidateScheduleConflicts(List<EmployeeWorkingHour> newWorkingHours,
        List<EmployeeWorkingHour> existingWorkingHours)
    {
        var allHours = newWorkingHours.Concat(existingWorkingHours);
        var hoursGroupedByDay = allHours.GroupBy(h => h.DayOfWeek);

        foreach (var day in hoursGroupedByDay)
        {
            var hasDayOff = day.Any(h => h.IsDayOff);
            var hasWorkingHours = day.Any(h => !h.IsDayOff);
            switch (hasDayOff)
            {
                case true when hasWorkingHours:
                    throw new BusinessRuleException(ResourceMessagesExceptions
                        .CANNOT_HAVE_DAY_OFF_WITH_WORKING_HOURS);
                case true:
                    continue;
            }

            var sortedHours = day.OrderBy(h => h.StartTime).ToList();
            for (var i = 0; i < sortedHours.Count - 1; i++)
            {
                if (sortedHours[i].EndTime > sortedHours[i + 1].StartTime)
                {
                    throw new BusinessRuleException(
                        ResourceMessagesExceptions.WORKING_HOURS_OVERLAP);
                }
            }
        }
    }

    private static void ValidateRequest(RequestEmployeeWorkingHourListJson request)
    {
        var validatorResult = new WorkingHourListValidator().Validate(request);
        if (!validatorResult.IsValid)
            throw new OnValidationException(validatorResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
}

