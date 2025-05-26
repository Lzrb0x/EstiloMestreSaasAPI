using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Employee.Register;

public class RegisterEmployeeUseCase : IRegisterEmployeeUseCase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _uof;

    public RegisterEmployeeUseCase(IEmployeeRepository employeeRepository, ILoggedUser loggedUser, IUnitOfWork uof)
    {
        _employeeRepository = employeeRepository;
        _loggedUser = loggedUser;
        _uof = uof;
    }

    public async Task<ResponseRegisteredEmployeeJson> Execute(long barbershopId)
    {
        if (barbershopId <= 0)
            throw new ErrorOnValidationException("Barbershop Id not found or invalid.");

        var loggedUser = await _loggedUser.User();

        var employee = await _employeeRepository.GetByUserId(loggedUser.Id);
        if (employee is not null)
        {
            return new ResponseRegisteredEmployeeJson
            {
                EmployeeId = employee.Id,
                BarbershopId = employee.BarberShopId
            };
        }

        employee = new Domain.Entities.Employee
        {
            UserId = loggedUser.Id,
            BarberShopId = barbershopId
        };

        await _employeeRepository.Add(employee);
        await _uof.Commit();

        return new ResponseRegisteredEmployeeJson
        {
            EmployeeId = employee.Id,
            BarbershopId = barbershopId
        };
    }
}
