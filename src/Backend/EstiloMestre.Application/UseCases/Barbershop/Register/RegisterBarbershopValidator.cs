using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Barbershop.Register;

public class RegisterBarbershopValidator : AbstractValidator<RequestRegisterBarbershopJson>
{
    public RegisterBarbershopValidator()
    {
        RuleFor(r => r.BarbershopName).NotEmpty().WithMessage(ResourceMessagesExceptions.BARBERSHOP_NAME_EMPTY);
        RuleFor(r => r.Address).NotEmpty().WithMessage(ResourceMessagesExceptions.ADDRESS_EMPTY);
    }
}
