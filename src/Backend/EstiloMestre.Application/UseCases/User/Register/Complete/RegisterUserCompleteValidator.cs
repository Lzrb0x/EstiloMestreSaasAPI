using EstiloMestre.Application.SharedValidators;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.User.Register.Complete;

public class RegisterUserCompleteValidator : AbstractValidator<RequestRegisterCompleteUserJson>
{
    public RegisterUserCompleteValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPTY);
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
        RuleFor(r => r.Password).SetValidator(new PasswordValidator<RequestRegisterCompleteUserJson>());
        When(r => string.IsNullOrWhiteSpace(r.Email) is false,
            () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
            });
        RuleFor(r => r.Phone).NotEmpty().WithMessage(ResourceMessagesExceptions.PHONE_NUMBER_EMPTY)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage(ResourceMessagesExceptions.PHONE_NUMBER_INVALID);
    }
}

//VALID NUMBERS
// +15551234567
// 15551234567
// +447911123456

// INVALID NUMBERS
// (555) 123-4567
// 555-123-4567
//     +1 555 123 4567
// abc123