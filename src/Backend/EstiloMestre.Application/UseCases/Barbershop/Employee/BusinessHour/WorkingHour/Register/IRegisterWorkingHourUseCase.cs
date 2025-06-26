using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Register;

public interface IRegisterWorkingHourUseCase
{
    Task Execute(RequestEmployeeWorkingHourListJson request, long employeeId);
}