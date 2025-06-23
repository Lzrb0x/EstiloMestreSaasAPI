using EstiloMestre.Application.SharedValidators;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.User.CompleteProfile;

public class
    CompletePartialUserProfileValidator : AbstractValidator<RequestCompletePartialUserProfileJson>
{
    public CompletePartialUserProfileValidator()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
        RuleFor(r => r.Password).SetValidator(new PasswordValidator<RequestCompletePartialUserProfileJson>());
        When(r => string.IsNullOrWhiteSpace(r.Email) is false,
            () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
            });
    }
}