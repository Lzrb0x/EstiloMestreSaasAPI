using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly EstiloMestreDbContext _context;

    public UserRepository(EstiloMestreDbContext context) => _context = context;


    public async Task Add(User user) => await _context.Users.AddAsync(user);


    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }
}