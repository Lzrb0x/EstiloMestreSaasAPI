using EstiloMestre.Application.SharedValidators;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHourOverride;

public class WorkingHourOverrideValidator : AbstractValidator<RequestWorkingHourOverrideJson>
{
    public WorkingHourOverrideValidator()
    {
        RuleFor(r => r.Date).NotEmpty().WithMessage(ResourceMessagesExceptions.WORKING_HOUR_OVERRIDE_DATE_REQUIRED)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage(ResourceMessagesExceptions.DATE_IN_THE_PAST);
        RuleFor(r => r).SetValidator(new BusinessHourValidator<RequestWorkingHourOverrideJson>(
            x => x.StartTime,
            x => x.EndTime,
            x => x.IsDayOff));
    }
}