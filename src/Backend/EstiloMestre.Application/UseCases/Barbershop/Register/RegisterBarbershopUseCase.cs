using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Register;

public class RegisterBarbershopUseCase : IRegisterBarbershopUseCase
{
    private readonly IBarbershopRepository _barbershopRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterBarbershopUseCase(
        IBarbershopRepository barbershopRepository,
        IOwnerRepository ownerRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _barbershopRepository = barbershopRepository;
        _ownerRepository = ownerRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredBarbershopJson> Execute(RequestRegisterBarbershopJson request)
    {
        ValidateRequest(request);

        var loggedUser = await _loggedUser.User();

        var owner = await _ownerRepository.GetByUserId(loggedUser.Id);
        if (owner is null)
        {
            owner = new Owner
            {
                UserId = loggedUser.Id,
                Barbershops = []
            };
            await _ownerRepository.Add(owner);
        }

        var barbershop = _mapper.Map<Domain.Entities.Barbershop>(request);
        barbershop.Owner = owner;

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
