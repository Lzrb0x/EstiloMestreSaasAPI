using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register;

public class RegisterBarbershopServiceValidator : AbstractValidator<RequestRegisterBarbershopServiceJson>
{
    public RegisterBarbershopServiceValidator()
    {
        RuleFor(r => r.BarbershopServices.Count)
            .GreaterThan(0)
            .WithMessage(ResourceMessagesExceptions.AT_LEAST_ONE_SERVICE_REQUIRED);
        RuleForEach(r => r.BarbershopServices)
            .ChildRules(serviceRule =>
            {
                serviceRule.RuleFor(service => service.ServiceId)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesExceptions.SERVICE_ID_INVALID);

                serviceRule.RuleFor(service => service.Price)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesExceptions.SERVICE_PRICE_INVALID);

                serviceRule.RuleFor(service => service.Duration)
                    .GreaterThan(TimeSpan.Zero)
                    .WithMessage(ResourceMessagesExceptions.SERVICE_DURATION_INVALID);

                serviceRule.RuleFor(service => service.DescriptionOverride)
                    .MaximumLength(300)
                    .WithMessage(ResourceMessagesExceptions.SERVICE_DESCRIPTION_TOO_LONG);
            });
    }
}
