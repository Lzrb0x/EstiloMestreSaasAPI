using AutoMapper;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register;

public class EmployeeRegistrationHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    public async Task<ResponseRegisteredEmployeeJson> RegisterEmployee(long userId, long barbershopId)
    {
        await ValidateEmployeeRegistration(userId, barbershopId);

        var employee = new Domain.Entities.Employee
        {
            UserId = userId,
            BarberShopId = barbershopId
        };

        await employeeRepository.Add(employee);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredEmployeeJson>(employee);
    }

    private async Task ValidateEmployeeRegistration(long userId, long barbershopId)
    {
        var existingEmployee = await employeeRepository.GetEmployeeByUserId(userId);
        if (existingEmployee != null)
        {
            if (existingEmployee.BarberShopId == barbershopId)
            {
                throw new BusinessRuleException(ResourceMessagesExceptions
                    .EMPLOYEE_ALREADY_REGISTERED_IN_THIS_BARBERSHOP);
            }

            throw new BusinessRuleException(ResourceMessagesExceptions
                .EMPLOYEE_ALREADY_REGISTERED_IN_ANOTHER_BARBERSHOP);
        }
    }
}