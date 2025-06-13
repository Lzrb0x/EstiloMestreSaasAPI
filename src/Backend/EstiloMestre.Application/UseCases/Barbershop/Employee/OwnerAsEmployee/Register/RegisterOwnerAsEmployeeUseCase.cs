using AutoMapper;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.OwnerAsEmployee.Register;

public class RegisterOwnerAsEmployeeUseCase(
    IEmployeeRepository employeeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRegisterOwnerAsEmployeeUseCase
{
    public async Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId)
    {
        var user = await loggedUser.User();

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
        await unitOfWork.Commit();
        
        return mapper.Map<ResponseRegisteredEmployeeJson>(employee);
    }
}