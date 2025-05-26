using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Employee.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class EmployeeController : EstiloMestreBaseController
{
    [Route("{barbershopId}")]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEmployeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployeeAsync(
        [FromRoute] long barbershopId,
        [FromServices] IRegisterEmployeeUseCase useCase
    )
    {
        var response = await useCase.Execute(barbershopId);

        return Created(string.Empty, response);
    }
}
