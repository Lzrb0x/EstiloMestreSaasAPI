using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.Booking;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BookingRepository(EstiloMestreDbContext dbContext) : IBookingRepository
{
    public async Task Add(Booking booking) => await dbContext.AddAsync(booking);

    public async Task<IList<Booking>> GetByEmployeeIdAndDate(long employeeId, DateOnly date)
    {
        return await dbContext.Bookings.Where(b => b.EmployeeId == employeeId && b.Date == date && b.Active)
            .ToListAsync();
    }
}