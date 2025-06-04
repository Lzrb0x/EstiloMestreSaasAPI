using AutoMapper;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Services.ILoggedUser;

namespace EstiloMestre.Application.UseCases.Owner.Register;

public class RegisterOwnerUseCase(IOwnerRepository repository, ILoggedUser user, IUnitOfWork uof, IMapper mapper)
    : IRegisterOwnerUseCase
{
    public async Task<ResponseRegisteredOwnerJson> Execute()
    {
        var loggedUser = await user.User();

        var owner = await repository.GetByUserId(loggedUser.Id);
        if (owner is not null)
        {
            return mapper.Map<ResponseRegisteredOwnerJson>(owner);
        }

        owner = new Domain.Entities.Owner
        {
            UserId = loggedUser.Id
        };

        await repository.Add(owner);
        await uof.Commit();

        return mapper.Map<ResponseRegisteredOwnerJson>(owner);
    }
}
