using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register.UserAsEmployee;

public class RegisterEmployeeUseCase(
    IUserRepository userRepository,
    EmployeeRegistrationHandler employeeRegistrationHandler
) : IRegisterEmployeeUseCase
{
    public async Task<ResponseRegisteredEmployeeJson> Execute(RequestRegisterEmployeeJson request, long barbershopId)
    {
        await ValidateRequest(request);

        var user = await userRepository.GetByEmail(request.Email)
            ?? throw new NotFoundException(ResourceMessagesExceptions.USER_NOT_FOUND);

        return await employeeRegistrationHandler.RegisterEmployee(user.Id, barbershopId);
    }

    private static async Task ValidateRequest(RequestRegisterEmployeeJson request)
    {
        var resultValidator = await new RegisterEmployeeValidator().ValidateAsync(request);
        if (resultValidator.IsValid is false)
            throw new OnValidationException(resultValidator.Errors.Select(e => e.ErrorMessage).ToList());
    }
}