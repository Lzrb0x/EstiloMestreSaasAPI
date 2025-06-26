using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Domain.Repositories.Employee.BusinessHour;

public interface IWorkingHourRepository
{
   Task AddRange(List<EmployeeWorkingHour> employeeWorkingHours);
   Task<List<EmployeeWorkingHour>> GetByEmployeeId(long employeeId);
}