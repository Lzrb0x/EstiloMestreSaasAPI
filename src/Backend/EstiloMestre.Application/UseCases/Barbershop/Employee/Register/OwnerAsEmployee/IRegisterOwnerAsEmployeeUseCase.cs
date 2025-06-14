using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register.OwnerAsEmployee;

public interface IRegisterOwnerAsEmployeeUseCase
{
    Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId);
}