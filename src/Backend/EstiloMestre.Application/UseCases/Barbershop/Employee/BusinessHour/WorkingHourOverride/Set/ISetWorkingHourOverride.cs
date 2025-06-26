using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHourOverride.Set;

public interface ISetWorkingHourOverrideUseCase
{
    Task Execute(RequestWorkingHourOverrideJson request, long employeeId);
}