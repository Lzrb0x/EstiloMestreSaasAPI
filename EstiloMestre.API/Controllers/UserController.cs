using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.User.Register;
using EstiloMestre.Application.UseCases.User.Register.Complete;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class UserController : EstiloMestreBaseController
{
    [HttpPost]
    [Route("complete-profile")]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUserComplete(
        [FromServices] IRegisterUserCompleteUseCase completeUseCase,
        [FromBody] RequestRegisterCompleteUserJson request)
    {
        var userRegistered = await completeUseCase.Execute(request);

        return Created(string.Empty, userRegistered);
    }
}