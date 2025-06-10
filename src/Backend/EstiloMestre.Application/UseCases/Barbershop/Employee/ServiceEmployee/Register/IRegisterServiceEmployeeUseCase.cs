using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.ServiceEmployee.Register;

public interface IRegisterServiceEmployeeUseCase
{
    Task<ResponseRegisteredServiceEmployeeJson> Execute(
        RequestRegisterServiceEmployeeJson request, long employeeId, long barbershopId
    );
}
