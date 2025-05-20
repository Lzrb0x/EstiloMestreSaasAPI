namespace EstiloMestre.Domain.Repositories.User;

public interface IUserRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmailAndPassword(string? email, string password);
    
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    
    Task Add(Entities.User user);

}