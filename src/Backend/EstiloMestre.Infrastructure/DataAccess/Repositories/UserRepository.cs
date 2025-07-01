using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class UserRepository(EstiloMestreDbContext context) : IUserRepository
{
    public async Task Add(User user) => await context.Users.AddAsync(user);

    public void Update(User user) => context.Users.Update(user);

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await context.Users.AsNoTracking().AnyAsync(u => u.Email!.Equals(email) && u.Active);
    }

    public async Task<User?> GetByEmailAndPassword(string? email, string password)
    {
        return await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email!.Equals(email) && u.Password!.Equals(password));
    }

    public async Task<User?> ExistActiveUserWithIdentifier(Guid userIdentifier)
    {
        return await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.Active);
    }

    public async Task<bool> UserProfileIsCompleteByUserIdentifier(Guid userIdentifier)
    {
        return await context.Users.AsNoTracking()
            .AnyAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.IsComplete);
    }
    
    public async Task<User?> GetUserByPhone(string phone)
    {
        return await context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Phone.Equals(phone) && u.Active);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Active == true && u.Email!.Equals(email));
    }

    public async Task<User?> GetUserByIdentifier(Guid userIdentifier)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.Active);
    }
}