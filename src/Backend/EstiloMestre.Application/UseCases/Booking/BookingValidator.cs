using EstiloMestre.Communication.Requests;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation;

namespace EstiloMestre.Application.UseCases.Booking;

public class BookingValidator : AbstractValidator<RequestCreateBookingJson>
{
    public BookingValidator()
    {
        RuleFor(r => r.BarbershopId)
            .GreaterThan(0)
            .WithMessage("Barbershop ID must be greater than 0.");

        RuleFor(r => r.BarbershopServiceId)
            .GreaterThan(0)
            .WithMessage("Barbershop Service ID must be greater than 0.");

        RuleFor(r => r.EmployeeId)
            .GreaterThan(0)
            .WithMessage("Employee ID must be greater than 0.");

        RuleFor(r => r)
            .Must(r => r.Date.ToDateTime(r.StartTime) > DateTime.UtcNow)
            .WithMessage("Não é possível realizar um agendamento para uma data e hora no passado.");
    }
}