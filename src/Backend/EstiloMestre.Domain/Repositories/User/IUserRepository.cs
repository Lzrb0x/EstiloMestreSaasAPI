namespace EstiloMestre.Domain.Repositories.User;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmailAndPassword(string? email, string password);
    Task<Entities.User?> ExistActiveUserWithIdentifier(Guid userIdentifier);
    Task<bool> UserProfileIsComplete(Guid userIdentifier);
    Task<Entities.User?> GetByEmail(string email);
}
