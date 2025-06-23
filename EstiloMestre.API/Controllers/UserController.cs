using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.User.CompleteProfile;
using EstiloMestre.Application.UseCases.User.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class UserController : EstiloMestreBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var userRegistered = await useCase.Execute(request);

        return Created(string.Empty, userRegistered);
    }

    [HttpPut]
    [Route("complete-profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CompleteProfile(
        [FromServices] ICompletePartialUserProfileUseCase useCase,
        RequestCompletePartialUserProfileJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
}