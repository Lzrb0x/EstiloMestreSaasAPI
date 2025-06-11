using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Service.Register;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class ServicesController : EstiloMestreBaseController
{
    // After we need to implement an attribute to validate if the user is an admin 

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredServiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        RequestServiceJson request,
        [FromServices] IRegisterServiceUseCase useCase
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}