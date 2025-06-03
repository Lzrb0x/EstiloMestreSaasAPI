using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.List;

public class RegisterBarbershopServiceListValidator : AbstractValidator<RequestRegisterBarbershopServiceListJson>
{
    public RegisterBarbershopServiceListValidator()
    {
        RuleFor(r => r.BarbershopServices.Count)
           .GreaterThan(0)
           .WithMessage(ResourceMessagesExceptions.AT_LEAST_ONE_SERVICE_REQUIRED);
        RuleForEach(r => r.BarbershopServices).SetValidator(new BarbershopServiceValidator());
    }
}
