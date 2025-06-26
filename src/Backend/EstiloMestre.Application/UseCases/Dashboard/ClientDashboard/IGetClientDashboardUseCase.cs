using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Dashboard.ClientDashboard;

public interface IGetClientDashboardUseCase
{
    Task<ResponseBarbershopJson> Execute();
}

