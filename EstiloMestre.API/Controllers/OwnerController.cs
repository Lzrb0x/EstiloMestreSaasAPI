using EstiloMestre.API.Attributes;
using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

[AuthenticatedUser]
public class OwnerController : EstiloMestreBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredOwnerJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterOwnerUseCase useCase)
    {
        var response = await useCase.Execute();

        return Created(string.Empty, response);
    }
}