namespace EstiloMestre.Domain.Repositories.Booking;

public interface IBookingRepository
{
    Task<IList<Entities.Booking>> GetByEmployeeIdAndDate(long employeeId, DateOnly date);
}