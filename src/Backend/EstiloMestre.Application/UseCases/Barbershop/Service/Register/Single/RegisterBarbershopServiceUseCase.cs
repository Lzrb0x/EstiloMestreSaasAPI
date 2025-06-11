using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Service;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;

public class RegisterBarbershopServiceUseCase(
    IBarbershopServiceRepository barbershopServicesRepository,
    IServiceRepository serviceRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRegisterBarbershopServiceUseCase
{
    public async Task<ResponseRegisteredBarbershopServiceJson> Execute(
        RequestBarbershopServiceJson request, long barbershopId
    )
    {
        ValidadeRequest(request);

        var globalServicesIds = await serviceRepository.GetGlobalServicesIds();

        if (!globalServicesIds.Contains(request.ServiceId))
            throw new BusinessRuleException(string.Format(ResourceMessagesExceptions.SERVICE_WITH_ID_NOT_FOUND,
                request.ServiceId));

        var servicesAlreadyRegisteredOnBarbershop = await barbershopServicesRepository
           .GetGlobalServicesAlreadyRegisteredOnBarbershop(barbershopId);

        if (servicesAlreadyRegisteredOnBarbershop.Contains(request.ServiceId))
            throw new BusinessRuleException(string.Format(ResourceMessagesExceptions.SERVICE_WITH_ID_ALREADY_REGISTERED,
                request.ServiceId));

        var barbershopService = mapper.Map<BarbershopService>(request);
        barbershopService.BarbershopId = barbershopId;

        await barbershopServicesRepository.Add(barbershopService);
        await unitOfWork.Commit();


        return mapper.Map<ResponseRegisteredBarbershopServiceJson>(barbershopService);
    }

    private static void ValidadeRequest(RequestBarbershopServiceJson request)
    {
        var result = new BarbershopServiceValidator().Validate(request);

        if (!result.IsValid) throw new OnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
