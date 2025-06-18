namespace EstiloMestre.Domain.Repositories.User;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmailAndPassword(string? email, string password);
    Task<Entities.User?> ExistActiveUserWithIdentifier(Guid userIdentifier);
    Task<bool> UserProfileIsCompleteByUserIdentifier(Guid userIdentifier);
    Task<Entities.User?> GetUserByPhone(string phone);
    Task<Entities.User?> GetUserByEmail(string email);
}
