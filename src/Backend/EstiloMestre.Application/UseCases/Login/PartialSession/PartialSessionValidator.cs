using EstiloMestre.Application.SharedValidators;
using EstiloMestre.Communication.Requests;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Login.PartialSession;

public class PartialSessionValidator : AbstractValidator<RequestPartialSessionUserJson>
{
    public PartialSessionValidator()
    {
        RuleFor(r => r.Phone).SetValidator(new PhoneValidator<RequestPartialSessionUserJson>());
    }
}