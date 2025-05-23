using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class OwnerController : EstiloMestreBaseController
{
    [HttpPost]
    [Route("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterOwner(
        [FromRoute] long userId,
        [FromServices] IRegisterOwnerUseCase useCase
    )
    {
        var response = await useCase.Execute(userId);

        return Created(string.Empty, response);
    }
}
