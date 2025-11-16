using AutoMapper;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Employee;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Get.GetByService;

public class GetEmployeesByService(
    IEmployeeRepository employeeRepository,
    IMapper mapper) : IGetEmployeesByService
{
    public async Task<ResponseEmployeesJson> Execute(long barbershopId, long barbershopServiceId)
    {
        var employees = await employeeRepository.GetEmployeesByBarbershopId(barbershopId);

        var employeesPerform =
            employees.Where(e => e.ServicesEmployee
                .Any(s => s.BarbershopServiceId == barbershopServiceId)).ToList();

        return new ResponseEmployeesJson
        {
            Employees = employeesPerform.Select(mapper.Map<EmployeeDto>).ToList()
        };
    }
}

