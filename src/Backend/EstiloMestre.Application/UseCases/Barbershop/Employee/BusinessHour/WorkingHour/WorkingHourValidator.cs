using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour;

public class WorkingHourValidator : AbstractValidator<RequestEmployeeWorkingHourJson>
{
    public WorkingHourValidator()
    {
        RuleFor(r => r.DayOfWeek)
            .IsInEnum()
            .WithMessage(ResourceMessagesExceptions.INVALID_DAY_OF_WEEK);
        RuleFor(r => r.StartTime).LessThan(r => r.EndTime).WithMessage(
                ResourceMessagesExceptions.START_TIME_MUST_BE_LESS_THAN_END_TIME)
            .When(r => !r.IsDayOff);
        RuleFor(x => x)
            .Must(x =>
                (x.IsDayOff && x.StartTime == default && x.EndTime == default) ||
                (!x.IsDayOff && x.StartTime != default && x.EndTime != default)
            )
            .WithMessage(ResourceMessagesExceptions.INVALID_WORKING_HOUR_CONFIGURATION);
    }
}

