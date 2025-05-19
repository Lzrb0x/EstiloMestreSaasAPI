using EstiloMestre.API.Attributes;
using EstiloMestre.Application.UseCases.Login.DoLogin;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class LoginController : EstiloMestreBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DoLogin([FromServices] IDoLoginUseCase useCase, RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
