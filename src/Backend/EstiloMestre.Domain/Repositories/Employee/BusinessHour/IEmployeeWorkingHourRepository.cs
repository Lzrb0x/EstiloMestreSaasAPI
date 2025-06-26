using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Domain.Repositories.Employee.BusinessHour;

public interface IEmployeeWorkingHourRepository
{
   Task AddRange(List<Entities.EmployeeWorkingHour> employeeWorkingHours);
   Task<List<EmployeeWorkingHour>> GetByEmployeeId(long employeeId);
}