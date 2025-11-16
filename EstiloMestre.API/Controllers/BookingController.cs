using EstiloMestre.API.Attributes;
using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Booking.Create;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class BookingController : EstiloMestreBaseController
{
    // [PartialOrCompletedUser]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBookingJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBooking(
        [FromBody] RequestCreateBookingJson request, [FromServices] ICreateBookingUseCase useCase)
    {
        var result = await useCase.Execute(request);

        var resourceUrl = $"/api/bookings/{result.Id}";

        return Created(resourceUrl, result);
    }
}