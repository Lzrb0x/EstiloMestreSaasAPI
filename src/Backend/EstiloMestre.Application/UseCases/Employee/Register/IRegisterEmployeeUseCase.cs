using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Employee.Register;

public interface IRegisterEmployeeUseCase
{
    Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId);
}
