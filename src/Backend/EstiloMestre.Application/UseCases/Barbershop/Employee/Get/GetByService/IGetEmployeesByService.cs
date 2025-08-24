using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Get.GetByService;

public interface IGetEmployeesByService
{
    Task<ResponseEmployeesJson> Execute(long barbershopId, long barbershopServiceId);
}