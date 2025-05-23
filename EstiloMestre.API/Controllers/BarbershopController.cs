using EstiloMestre.API.Attributes;
using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Barbershop.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

[AuthenticatedUser]
public class BarbershopController : EstiloMestreBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredBarbershopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBarbershop(
        [FromServices] IRegisterBarbershopUseCase useCase,
        [FromBody] RequestRegisterBarbershopJson request
    )
    {
        var barbershopRegistered = await useCase.Execute(request);
        return Created(string.Empty, barbershopRegistered);
    }
}
