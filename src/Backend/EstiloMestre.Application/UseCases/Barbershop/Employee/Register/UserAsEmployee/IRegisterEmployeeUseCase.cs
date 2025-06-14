using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register.UserAsEmployee;

public interface IRegisterEmployeeUseCase
{
    Task<ResponseRegisteredEmployeeJson> Execute(RequestRegisterEmployeeJson request, long barbershopId);
}
