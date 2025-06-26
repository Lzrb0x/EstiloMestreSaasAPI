using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.ServiceEmployee;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.ServiceEmployee.Register;

public class RegisterServiceEmployeeUseCase(
    IBarbershopServiceRepository barbershopServiceRepository,
    IEmployeeRepository employeeRepository,
    IServiceEmployeeRepository serviceEmployeeRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : IRegisterServiceEmployeeUseCase
{
    public async Task<ResponseRegisteredServiceEmployeeJson> Execute(
        RequestRegisterServiceEmployeeJson request, long barbershopId, long employeeId
    )
    {
        if (!await employeeRepository.ExistEmployeeById(employeeId))
            throw new NotFoundException(ResourceMessagesExceptions.EMPLOYEE_NOT_FOUND);

        var barbershopServicesIds = await barbershopServiceRepository
            .GetBarbershopServicesIds(barbershopId);

        var barbershopServiceExist = barbershopServicesIds.Contains(request.BarbershopServiceId);
        if (!barbershopServiceExist)
            throw new NotFoundException(ResourceMessagesExceptions.BARBERSHOP_SERVICE_WITH_ID_NOT_FOUND);

        var registeredBarbershopServicesByEmployeeId = await employeeRepository
            .GetRegisteredBarbershopServicesByEmployeeId(employeeId);

        var employeeAlreadyPerformBarbershopService =
            registeredBarbershopServicesByEmployeeId.Contains(request.BarbershopServiceId);
        if (employeeAlreadyPerformBarbershopService)
            throw new BusinessRuleException(ResourceMessagesExceptions.EMPLOYEE_SERVICE_ALREADY_REGISTERED);

        var serviceEmployee = mapper.Map<Domain.Entities.ServiceEmployee>(request);
        serviceEmployee.EmployeeId = employeeId;

        await serviceEmployeeRepository.Add(serviceEmployee);
        await unitOfWork.Commit();

        return new ResponseRegisteredServiceEmployeeJson
        {
            EmployeeId = serviceEmployee.EmployeeId,
            BarbershopServiceId = serviceEmployee.BarbershopServiceId
        };
    }
}