using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop;

public class CreateBarbershopUseCase : ICreateBarbershopUseCase
{
    private readonly IBarbershopRepository _barbershopRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public CreateBarbershopUseCase(
        IBarbershopRepository barbershopRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser
    )
    {
        _barbershopRepository = barbershopRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredBarbershopJson> Execute(RequestCreateBarbershopJson request)
    {
        ValidateRequest(request);

        var loggedUser = await _loggedUser.User();
        var user = await _userRepository.GetById(loggedUser.Id);

        var barbershop = new Domain.Entities.Barbershop
        {
            BarbershopName = request.BarbershopName, Phone = request.Phone, Address = request.Address, UserId = user.Id
        };

        if (user.IsOwner == false)
        {
            user.IsOwner = true;
            _userRepository.Update(user);
        }

        await _barbershopRepository.Add(barbershop);
        await _unitOfWork.Commit();

        return new ResponseRegisteredBarbershopJson { BarbershopName = barbershop.BarbershopName };
    }

    private static void ValidateRequest(RequestCreateBarbershopJson request)
    {
        var result = new CreateBarbershopValidator().Validate(request);
        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
