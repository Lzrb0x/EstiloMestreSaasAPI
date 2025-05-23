using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Owner;

namespace EstiloMestre.Application.UseCases.Owner.Register;

public class RegisterOwnerUseCase : IRegisterOwnerUseCase
{
    private readonly IOwnerRepository _repository;
    private readonly IUnitOfWork _uof;

    public RegisterOwnerUseCase(IOwnerRepository repository, IUnitOfWork uof)
    {
        _repository = repository;
        _uof = uof;
    }

    public async Task<ResponseRegisteredOwnerJson> Execute(long userId)
    {
        var owner = await _repository.GetByUserId(userId);
        if (owner is not null)
        {
            return new ResponseRegisteredOwnerJson
            {
                OwnerId = owner.Id
            };
        }

        owner = new Domain.Entities.Owner
        {
            UserId = userId
        };

        await _repository.Add(owner);
        await _uof.Commit();

        return new ResponseRegisteredOwnerJson
        {
            OwnerId = owner.Id
        };
    }
}
