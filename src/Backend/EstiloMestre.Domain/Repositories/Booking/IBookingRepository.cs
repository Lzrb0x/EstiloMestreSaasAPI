namespace EstiloMestre.Domain.Repositories.Booking;

public interface IBookingRepository
{
    Task Add(Entities.Booking booking);
    Task<IList<Entities.Booking>> GetByEmployeeIdAndDate(long employeeId, DateOnly date);
}