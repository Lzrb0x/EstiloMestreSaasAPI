using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Domain.Repositories.Employee.BusinessHour;

public interface IWorkingHourOverrideRepository
{
    Task Add(EmployeeWorkingHourOverride employeeWorkingHourOverride);
    Task<List<EmployeeWorkingHourOverride>> GetByEmployeeId(long employeeId);
}