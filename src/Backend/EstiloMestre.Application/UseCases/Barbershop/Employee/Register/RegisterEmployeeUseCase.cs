using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Register;

public class RegisterEmployeeUseCase : IRegisterEmployeeUseCase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public RegisterEmployeeUseCase(
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IUnitOfWork uof,
        IMapper mapper
    )
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredEmployeeJson> Execute(RequestRegisterEmployeeJson request, long barbershopId)
    {
        await ValidateRequest(request);

        var user = await _userRepository.GetByEmail(request.Email);
        if (user is null) throw new NotFoundException(ResourceMessagesExceptions.USER_NOT_FOUND);

        if (await _employeeRepository.ExistRegisteredEmployeeWithUserId(user.Id))
            throw new BusinessRuleException(ResourceMessagesExceptions.EMPLOYEE_ALREADY_REGISTERED);

        var employee = new Domain.Entities.Employee
        {
            UserId = user.Id,
            BarberShopId = barbershopId
        };

        await _employeeRepository.Add(employee);
        await _uof.Commit();

        return _mapper.Map<ResponseRegisteredEmployeeJson>(employee);
    }

    private static async Task ValidateRequest(RequestRegisterEmployeeJson request)
    {
        var resultValidator = await new RegisterEmployeeValidator().ValidateAsync(request);
        if (resultValidator.IsValid is false)
            throw new OnValidationException(resultValidator.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
