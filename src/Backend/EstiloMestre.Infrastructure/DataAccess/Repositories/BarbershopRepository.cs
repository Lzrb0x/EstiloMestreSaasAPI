using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Barbershop;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopRepository(EstiloMestreDbContext dbContext) : IBarbershopRepository
{
    public async Task Add(Barbershop barbershop)
    {
        await dbContext.Barbershops.AddAsync(barbershop);
    }

    public async Task<IList<Barbershop>> GetForDashboard()
    {
        return await dbContext.Barbershops.AsNoTracking().OrderByDescending(x => x.Id).Take(3).ToListAsync();
    }

    public async Task<bool> UserIsBarbershopOwner(long userId, long barbershopId)
    {
        return await dbContext.Barbershops.Include(b => b.Owner)
            .AsNoTracking()
            .AnyAsync(b =>
                b.Id == barbershopId && b.Active == true && b.Owner.UserId == userId && b.Owner.Active == true);
    }


    public async Task<bool> UserIsOwnerOrEmployee(long userId, long barbershopId, long employeeId)
    {
        return await dbContext.Barbershops
            .AsNoTracking()
            .Where(b => b.Id == barbershopId && b.Active == true)
            .AnyAsync(b =>
                b.Owner.UserId == userId && b.Owner.Active == true &&
                b.Employees.Any(e => e.Id == employeeId && e.Active == true)
                || b.Employees.Any(e => e.UserId == userId && e.Id == employeeId && e.Active == true));
    }

    public async Task<Barbershop?> GetBarbershopDetails(long barbershopId)
    {
        return await dbContext.Barbershops
            .Include(e => e.Employees)
            .ThenInclude(e => e.User)
            .Include(e => e.Services)
            .Include(e => e.Bookings)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == barbershopId && e.Active == true);
    }
}