using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Booking.Create;

public interface ICreateBookingUseCase
{
    Task<ResponseBookingJson> Execute(RequestCreateBookingJson request);
}