using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Service;

public class ServiceValidator : AbstractValidator<RequestServiceJson>
{
    public ServiceValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.SERVICE_NAME_EMPTY);
        RuleFor(r => r.Name).MaximumLength(100).WithMessage(ResourceMessagesExceptions.SERVICE_NAME_TOO_LONG);
        RuleFor(r => r.Description)
            .MaximumLength(300)
            .WithMessage(ResourceMessagesExceptions.SERVICE_DESCRIPTION_TOO_LONG);
    }
}
