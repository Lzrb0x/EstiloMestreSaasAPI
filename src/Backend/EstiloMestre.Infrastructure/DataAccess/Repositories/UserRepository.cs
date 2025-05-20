using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EstiloMestreDbContext _context;
    public UserRepository(EstiloMestreDbContext context) => _context = context;

    public async Task Add(User user) => await _context.Users.AddAsync(user);


    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }

    public async Task<User?> GetByEmailAndPassword(string? email, string password)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
    {
        return await _context.Users.AsNoTracking().AnyAsync(u => u.UserIdentifier.Equals(userIdentifier) && u.Active);
    }
}
