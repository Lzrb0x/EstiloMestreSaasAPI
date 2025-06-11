using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register;

public class RegisterEmployeeUseCase(
    IEmployeeRepository employeeRepository,
    IUserRepository userRepository,
    IUnitOfWork uof,
    IMapper mapper
) : IRegisterEmployeeUseCase
{
    public async Task<ResponseRegisteredEmployeeJson> Execute(RequestRegisterEmployeeJson request, long barbershopId)
    {
        await ValidateRequest(request);

        var user = await userRepository.GetByEmail(request.Email);
        if (user is null) throw new NotFoundException(ResourceMessagesExceptions.USER_NOT_FOUND);

        if (await employeeRepository.ExistRegisteredEmployeeWithUserIdAndBarbershopId(user.Id, barbershopId))
            throw new BusinessRuleException(ResourceMessagesExceptions.EMPLOYEE_ALREADY_REGISTERED_IN_THIS_BARBERSHOP);

        if (await employeeRepository.ExistRegisteredEmployeeWithUserId(user.Id))
            throw new BusinessRuleException(
                ResourceMessagesExceptions.EMPLOYEE_ALREADY_REGISTERED_IN_ANOTHER_BARBERSHOP);

        var employee = new Domain.Entities.Employee
        {
            UserId = user.Id,
            BarberShopId = barbershopId
        };

        await employeeRepository.Add(employee);
        await uof.Commit();

        return mapper.Map<ResponseRegisteredEmployeeJson>(employee);
    }

    private static async Task ValidateRequest(RequestRegisterEmployeeJson request)
    {
        var resultValidator = await new RegisterEmployeeValidator().ValidateAsync(request);
        if (resultValidator.IsValid is false)
            throw new OnValidationException(resultValidator.Errors.Select(e => e.ErrorMessage).ToList());
    }
}