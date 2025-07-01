using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Slots;

public interface IGetEmployeeSlotsUseCase
{
    Task<ResponseEmployeeSlotsJson> Execute(long employeeId, DateOnly date, long barbershopServiceId);
}
