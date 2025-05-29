using EstiloMestre.API.Attributes;
using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register;
using EstiloMestre.Application.UseCases.Barbershop.Register;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class BarbershopController : EstiloMestreBaseController
{
    [AuthenticatedUser]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredBarbershopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBarbershop(
        [FromServices] IRegisterBarbershopUseCase useCase,
        [FromBody] RequestRegisterBarbershopJson request
    )
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [BarbershopOwner]
    [HttpPost]
    [Route("{barbershopId:long}/employees")]
    [ProducesResponseType(typeof(ResponseRegisteredEmployeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEmployee(
        [FromRoute] long barbershopId,
        [FromBody] RequestRegisterEmployeeJson request,
        [FromServices] IRegisterEmployeeUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId);
        return Created(string.Empty, response);
    }

    [BarbershopOwner]
    [HttpPost]
    [Route("{barbershopId:long}/services")]
    [ProducesResponseType(typeof(ResponseRegisteredBarbershopServiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterBarbershopService(
        [FromRoute] long barbershopId,
        [FromBody] RequestRegisterBarbershopServiceJson request,
        [FromServices] IRegisterBarbershopServiceUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId);
        return Created(string.Empty, response);
    }
}
