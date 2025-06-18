using System.Text.RegularExpressions;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;
using FluentValidation.Validators;

namespace EstiloMestre.Application.SharedValidators;

public class PhoneValidator<T> : PropertyValidator<T, string>
{
    private static readonly Regex PhoneRegex = new(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$", RegexOptions.Compiled);

    public override bool IsValid(ValidationContext<T> context, string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage",
                ResourceMessagesExceptions.PHONE_NUMBER_EMPTY);
            return false;
        }

        if (!PhoneRegex.IsMatch(phone))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage",
                ResourceMessagesExceptions.PHONE_NUMBER_INVALID);
            return false;
        }

        return true;
    }

    public override string Name => nameof(PhoneValidator<T>);

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}

// valid numbers example:
// (11) 99876-5432
// 11 99876-5432
// 11998765432
// (62)99876-5432
// 62998765432