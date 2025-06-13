using AutoMapper;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Register;

public class RegisterBarbershopUseCase(
    IBarbershopRepository barbershopRepository,
    IOwnerRepository ownerRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRegisterBarbershopUseCase
{
    public async Task<ResponseRegisteredBarbershopJson> Execute(RequestRegisterBarbershopJson request)
    {
        ValidateRequest(request);

        var user = await loggedUser.User();
        var owner = await ownerRepository.GetByUserId(user.Id); //filter already validated 
        var barbershop = mapper.Map<Domain.Entities.Barbershop>(request);
        barbershop.OwnerId = owner!.Id;

        await barbershopRepository.Add(barbershop);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredBarbershopJson>(barbershop);
    }

    private static void ValidateRequest(RequestRegisterBarbershopJson request)
    {
        var result = new RegisterBarbershopValidator().Validate(request);
        if (!result.IsValid) throw new OnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
