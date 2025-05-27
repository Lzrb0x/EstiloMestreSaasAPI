using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register;

public class RegisterEmployeeValidator : AbstractValidator<RequestRegisterEmployeeJson>
{
    public RegisterEmployeeValidator()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
        When(r => string.IsNullOrWhiteSpace(r.Email) is false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
        });
    }
}
