using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Set;

public class WorkingHourListValidator : AbstractValidator<RequestEmployeeWorkingHourListJson>
{
    public WorkingHourListValidator()
    {
        RuleFor(r => r.WorkingHours.Count).GreaterThan(0)
            .WithMessage(ResourceMessagesExceptions.AT_LEAST_ONE_WORKING_HOURS_REQUIRED);
        RuleForEach(r => r.WorkingHours).SetValidator(new WorkingHourValidator());
    }
}