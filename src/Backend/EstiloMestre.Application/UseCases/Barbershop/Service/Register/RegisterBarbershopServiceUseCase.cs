using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Service;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register;

public class RegisterBarbershopServiceUseCase(
    IBarbershopServiceRepository barbershopServicesRepository,
    IServiceRepository serviceRepository,
    IUnitOfWork unitOfWork
) : IRegisterBarbershopServiceUseCase
{
    public async Task<ResponseRegisteredBarbershopServiceJson> Execute(
        RequestRegisterBarbershopServiceJson request, long barbershopId
    )
    {
        ValidateRequest(request);
        
        

        
        //ORDEM:
        
        // 1- filtro para ignorar serviços com id duplicados.
        
        // 2- verificar se o Id do serviço é válido(cadastrado como global.), caso não seja, lançar exceção
        // com os ids inválidos.
        
        // verificar se o serviço já está cadastrado na barbearia, caso estiver, lançar exceção.
        
        
        
        
        
        

        // await barbershopServicesRepository.AddRange(barbershopServices);
        await unitOfWork.Commit();

        return new ResponseRegisteredBarbershopServiceJson
        {
            Name = "Serviços cadastrados com sucesso!"
        };
    }

    private static void ValidateRequest(RequestRegisterBarbershopServiceJson request)
    {
        var result = new RegisterBarbershopServiceValidator().Validate(request);

        if (result.IsValid is false)
            throw new OnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
    }
}
