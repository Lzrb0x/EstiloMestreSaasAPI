using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;
using FluentValidation.Validators;

namespace EstiloMestre.Application.SharedValidators;

public class BusinessHourValidator<T>(
    Func<T, TimeOnly> getStartTime,
    Func<T, TimeOnly> getEndTime,
    Func<T, bool> getIsDayOff)
    : PropertyValidator<T, T>
{
    public override bool IsValid(ValidationContext<T> context, T value)
    {
        var startTime = getStartTime(value);
        var endTime = getEndTime(value);
        var isDayOff = getIsDayOff(value);

        if (!isDayOff && startTime >= endTime)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage",
                ResourceMessagesExceptions.START_TIME_MUST_BE_LESS_THAN_END_TIME);
            return false;
        }

        var isValidDayOff = isDayOff && startTime == default && endTime == default;
        var isValidWorkingDay = !isDayOff && startTime != default && endTime != default;

        if (isValidDayOff || isValidWorkingDay) return true;
        context.MessageFormatter.AppendArgument("ErrorMessage",
            ResourceMessagesExceptions.INVALID_WORKING_HOUR_CONFIGURATION);
        return false;
    }

    public override string Name => nameof(BusinessHourValidator<T>);

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}