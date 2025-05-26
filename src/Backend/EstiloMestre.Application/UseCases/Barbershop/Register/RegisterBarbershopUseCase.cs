using AutoMapper;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Register;

public class RegisterBarbershopUseCase : IRegisterBarbershopUseCase
{
    private readonly IBarbershopRepository _barbershopRepository;
    private readonly IRegisterOwnerUseCase _registerOwnerUseCase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterBarbershopUseCase(
        IBarbershopRepository barbershopRepository,
        IRegisterOwnerUseCase registerOwnerUseCase,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _barbershopRepository = barbershopRepository;
        _registerOwnerUseCase = registerOwnerUseCase;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredBarbershopJson> Execute(RequestRegisterBarbershopJson request)
    {
        ValidateRequest(request);

        var owner = await _registerOwnerUseCase.Execute();

        var barbershop = _mapper.Map<Domain.Entities.Barbershop>(request);
        barbershop.OwnerId = owner.OwnerId;

        await _barbershopRepository.Add(barbershop);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredBarbershopJson>(barbershop);
    }

    private static void ValidateRequest(RequestRegisterBarbershopJson request)
    {
        var result = new RegisterBarbershopValidator().Validate(request);
        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
