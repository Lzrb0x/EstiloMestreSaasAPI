using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Services.ILoggedUser;

namespace EstiloMestre.Application.UseCases.Owner.Register;

public class RegisterOwnerUseCase : IRegisterOwnerUseCase
{
    private readonly IOwnerRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _uof;

    public RegisterOwnerUseCase(IOwnerRepository repository, ILoggedUser loggedUser, IUnitOfWork uof)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _uof = uof;
    }

    public async Task<ResponseRegisteredOwnerJson> Execute()
    {
        var loggedUser = await _loggedUser.User();

        var owner = await _repository.GetByUserId(loggedUser.Id);
        if (owner is not null)
        {
            return new ResponseRegisteredOwnerJson
            {
                OwnerId = owner.Id
            };
        }

        owner = new Domain.Entities.Owner
        {
            UserId = loggedUser.Id
        };

        await _repository.Add(owner);
        await _uof.Commit();

        return new ResponseRegisteredOwnerJson
        {
            OwnerId = owner.Id
        };
    }
}
