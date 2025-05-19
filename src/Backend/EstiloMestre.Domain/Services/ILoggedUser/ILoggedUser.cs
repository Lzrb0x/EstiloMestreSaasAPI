using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Domain.Services.ILoggedUser;

public interface ILoggedUser
{
    Task<User> User();
}
