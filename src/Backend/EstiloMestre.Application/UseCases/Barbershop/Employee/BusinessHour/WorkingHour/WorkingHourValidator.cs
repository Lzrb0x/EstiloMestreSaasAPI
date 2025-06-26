using EstiloMestre.Application.SharedValidators;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour;

public class WorkingHourValidator : AbstractValidator<RequestWorkingHourJson>
{
    public WorkingHourValidator()
    {
        RuleFor(r => r.DayOfWeek)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.INVALID_DAY_OF_WEEK);
        RuleFor(r => r).SetValidator(new BusinessHourValidator<RequestWorkingHourJson>(
            x => x.StartTime,
            x => x.EndTime,
            x => x.IsDayOff));
    }
}

