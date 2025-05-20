namespace EstiloMestre.Domain.Repositories.User;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmailAndPassword(string? email, string password);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    Task<Entities.User> GetById(long userId);
    
    void Update(Entities.User user);
}
