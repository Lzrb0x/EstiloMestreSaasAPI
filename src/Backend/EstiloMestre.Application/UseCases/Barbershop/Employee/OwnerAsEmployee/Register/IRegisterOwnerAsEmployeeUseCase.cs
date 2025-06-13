using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.OwnerAsEmployee.Register;

public interface IRegisterOwnerAsEmployeeUseCase
{
    Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId);
}