using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Services.ILoggedUser;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register.OwnerAsEmployee;

public class RegisterOwnerAsEmployeeUseCase(
    ILoggedUser loggedUser,
    EmployeeRegistrationHandler employeeRegistrationHandler)
    : IRegisterOwnerAsEmployeeUseCase
{
    public async Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId)
    {
        var user = await loggedUser.User();
        
        return await employeeRegistrationHandler.RegisterEmployee(user.Id, barbershopId);
    }
}