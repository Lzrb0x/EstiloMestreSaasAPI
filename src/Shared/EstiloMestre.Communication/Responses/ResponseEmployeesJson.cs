using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseEmployeesJson
{
   public IList<EmployeeDto> Employees { get; set; } = [];
}