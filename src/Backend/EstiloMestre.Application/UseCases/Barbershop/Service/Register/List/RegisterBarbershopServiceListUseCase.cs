using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Service;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.List;

public class RegisterBarbershopServiceListUseCase(
    IBarbershopServiceRepository barbershopServicesRepository,
    IServiceRepository serviceRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRegisterBarbershopServiceListUseCase
{
    public async Task<ResponseRegisteredBarbershopServiceJson> Execute(
        RequestRegisterBarbershopServiceListJson request, long barbershopId
    )
    {
        ValidateRequest(request);

        var servicesDtoFiltered = request.BarbershopServices.DistinctBy(service => service.ServiceId).ToList();

        var globalServicesIds = await serviceRepository.GetServicesIds();

        var notFoundServices = servicesDtoFiltered.Where(s => !globalServicesIds.Contains(s.ServiceId)).ToList();
        if (notFoundServices.Any())
        {
            var notFoundIds = string.Join(", ", notFoundServices.Select(s => s.ServiceId));
            throw new BusinessRuleException(string.Format(ResourceMessagesExceptions.SERVICE_WITH_ID_NOT_FOUND,
                notFoundIds));
        }

        var barbershopServicesAlreadyRegistered = await barbershopServicesRepository
           .GetBarbershopServicesIdsByBarbershopId(barbershopId);
        if (barbershopServicesAlreadyRegistered.Any())
        {
            var servicesAlreadyRegistered = servicesDtoFiltered
               .Where(s => barbershopServicesAlreadyRegistered.Contains(s.ServiceId))
               .ToList();

            if (servicesAlreadyRegistered.Any())
            {
                var alreadyRegisteredIds = string.Join(", ", servicesAlreadyRegistered.Select(s => s.ServiceId));
                throw new BusinessRuleException(
                    string.Format(ResourceMessagesExceptions.SERVICE_WITH_ID_ALREADY_REGISTERED, alreadyRegisteredIds));
            }
        }

        var barbershopServices = mapper.Map<List<BarbershopService>>(servicesDtoFiltered);
        barbershopServices.ForEach(service =>
        {
            service.BarbershopId = barbershopId;
        });

        await barbershopServicesRepository.AddRange(barbershopServices);
        await unitOfWork.Commit();

        return new ResponseRegisteredBarbershopServiceJson
        {
            Message = "Serviços cadastrados com sucesso!"
        };
    }

    private static void ValidateRequest(RequestRegisterBarbershopServiceListJson request)
    {
        var result = new RegisterBarbershopServiceListValidator().Validate(request);

        if (!result.IsValid)
            throw new OnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
    }
}
