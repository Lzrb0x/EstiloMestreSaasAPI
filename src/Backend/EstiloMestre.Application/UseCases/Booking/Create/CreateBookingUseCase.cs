using EstiloMestre.Application.UseCases.Barbershop.Employee.Slots;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Booking;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.ServiceEmployee;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Booking.Create;

public class CreateBookingUseCase(
    ILoggedUser loggedUser,
    IEmployeeRepository employeeRepository,
    IBarbershopServiceRepository barbershopServiceRepository,
    IServiceEmployeeRepository serviceEmployeeRepository,
    IGetEmployeeSlotsUseCase getEmployeeSlotsUseCase,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork) : ICreateBookingUseCase
{
    public async Task<ResponseBookingJson> Execute(RequestCreateBookingJson request)
    {
        ValidateRequest(request);

        var loggedCustomer = await loggedUser.User();

        var (employee, service) = await LoadAndValidateCoreEntities(request);

        await ValidateSlotIsStillAvailable(request);

        var bookingEndTime = request.StartTime.Add(service.Duration);

        var newBooking = new Domain.Entities.Booking
        {
            BarbershopId = request.BarbershopId,
            CustomerId = loggedCustomer.Id,
            EmployeeId = employee.Id,
            BarbershopServiceId = service.Id,
            StartTime = request.StartTime,
            EndTime = bookingEndTime,
            Date = request.Date
        };

        await bookingRepository.Add(newBooking);
        await unitOfWork.Commit();

        return new ResponseBookingJson
        {
            Id = newBooking.Id,
            Date = newBooking.Date,
            StartTime = newBooking.StartTime,
            EndTime = newBooking.EndTime,
        };
    }

    private async Task<(Employee employee, BarbershopService barbershopService)> LoadAndValidateCoreEntities(
        RequestCreateBookingJson request)
    {
        var employee = await employeeRepository.GetEmployeeById(request.EmployeeId) ??
                       throw new NotFoundException(ResourceMessagesExceptions.EMPLOYEE_NOT_FOUND);

        var barbershopService = await barbershopServiceRepository.GetById(request.BarbershopServiceId) ??
                                throw new NotFoundException(ResourceMessagesExceptions
                                    .BARBERSHOP_SERVICE_WITH_ID_NOT_FOUND);

        if (request.BarbershopId != employee.BarberShopId || request.BarbershopId != barbershopService.BarbershopId)
            throw new BusinessRuleException(
                "O funcionário ou o serviço solicitado não pertencem à barbearia.");

        var employeePerformTheService = await serviceEmployeeRepository.EmployeePerformsService(
            request.EmployeeId, request.BarbershopServiceId);

        if (!employeePerformTheService)
            throw new BusinessRuleException("o Funcionário selecionado não realiza o serviço solicitado.");

        return (employee, barbershopService);
    }

    private async Task ValidateSlotIsStillAvailable(RequestCreateBookingJson request)
    {
        var availableSlotsResponse =
            await getEmployeeSlotsUseCase.Execute(request.EmployeeId, request.Date, request.BarbershopServiceId);

        var isSlotsAvailable = availableSlotsResponse.Slots.Any(slot => slot.Time == request.StartTime);
        if (!isSlotsAvailable)
            throw new BusinessRuleException("O horário solicitado não está mais disponível.");
    }

    private static void ValidateRequest(RequestCreateBookingJson request)
    {
        var validatorResult = new BookingValidator().Validate(request);
        if (!validatorResult.IsValid)
            throw new OnValidationException(validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
    }
}