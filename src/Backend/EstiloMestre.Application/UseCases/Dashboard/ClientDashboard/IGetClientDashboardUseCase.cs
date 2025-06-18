using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Application.UseCases.Dashboard.ClientDashboard;

public interface IGetClientDashboardUseCase
{
    Task<ResponseBarbershopJson> Execute();
}

