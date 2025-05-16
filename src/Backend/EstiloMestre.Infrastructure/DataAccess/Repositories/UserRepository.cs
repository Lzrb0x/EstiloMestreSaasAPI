using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.User;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository
{
    private readonly EstiloMestreDbContext _context;

    public UserRepository(EstiloMestreDbContext context) => _context = context;


    public async Task Add(User user) => await _context.Users.AddAsync(user);
}


//falta adicionar string de conexão do banco atual e tentar persistir um 'User'