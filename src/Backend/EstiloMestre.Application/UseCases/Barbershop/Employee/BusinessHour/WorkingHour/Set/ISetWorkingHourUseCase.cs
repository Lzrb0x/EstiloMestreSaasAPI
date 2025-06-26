using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Set;

public interface ISetWorkingHourUseCase
{
    Task Execute(RequestEmployeeWorkingHourListJson request, long employeeId);
}