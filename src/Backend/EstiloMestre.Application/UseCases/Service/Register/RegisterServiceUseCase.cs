using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Service;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace EstiloMestre.Application.UseCases.Service.Register;

public class RegisterServiceUseCase(IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRegisterServiceUseCase
{
    public async Task<ResponseRegisteredServiceJson> Execute(RequestServiceJson request)
    {
        await ValidadeRequest(request);

        var service = mapper.Map<Domain.Entities.Service>(request);

        await serviceRepository.Add(service);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredServiceJson>(service);
    }

    private async Task ValidadeRequest(RequestServiceJson request)
    {
        var result = await new ServiceValidator().ValidateAsync(request);

        if (await serviceRepository.ExistGlobalServiceByName(request.Name))
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.SERVICE_ALREADY_EXISTS));
        }

        if (result.IsValid is false)
        {
            throw new OnValidationException(result.Errors.Select(x => x.ErrorMessage).Distinct().ToList());
        }
    }
}
