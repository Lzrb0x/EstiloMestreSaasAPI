using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Dashboard.ClientDashboard;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class ClientDashboardController : EstiloMestreBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseBarbershopJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDashboard(
        [FromServices] IGetClientDashboardUseCase useCase
    )
    {
        var response = await useCase.Execute();
        return Ok(response);
    }
}