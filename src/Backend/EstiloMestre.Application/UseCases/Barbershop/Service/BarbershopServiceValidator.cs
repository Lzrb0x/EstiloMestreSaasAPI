using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Service;

public class BarbershopServiceValidator : AbstractValidator<RequestBarbershopServiceJson>
{
    public BarbershopServiceValidator()
    {
        RuleFor(service => service.ServiceId).GreaterThan(0).WithMessage(ResourceMessagesExceptions.SERVICE_ID_INVALID);

        RuleFor(service => service.Price).GreaterThan(0).WithMessage(ResourceMessagesExceptions.SERVICE_PRICE_INVALID);

        RuleFor(service => service.Duration)
           .GreaterThan(TimeSpan.Zero)
           .WithMessage(ResourceMessagesExceptions.SERVICE_DURATION_INVALID);

        RuleFor(service => service.DescriptionOverride)
           .MaximumLength(300)
           .WithMessage(ResourceMessagesExceptions.SERVICE_DESCRIPTION_TOO_LONG);
    }
}
