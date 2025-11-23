using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Barbershop;

namespace EstiloMestre.Application.UseCases.Barbershop.BarbershopDetails.Get;

public class GetBarbershopDetailsUseCase(IBarbershopRepository barbershopRepository) : IGetBarbershopDetailsUseCase
{
    public async Task<ResponseBarbershopDetailsJson> Execute(long barbershopId)
    {
        var barbershopDetails = await barbershopRepository.GetBarbershopDetails(barbershopId);
        if (barbershopDetails == null)
            throw new Exception($"Barbershop with id {barbershopId} not found.");

        return new ResponseBarbershopDetailsJson
        {
            Id = barbershopDetails.Id,
            BarbershopName = barbershopDetails.BarbershopName,
            Address = barbershopDetails.Address,
            Phone = barbershopDetails.Phone,
            OwnerId = barbershopDetails.OwnerId,
            Employees = new ResponseEmployeesJson
            {
                Employees = barbershopDetails.Employees.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    Name = e.User.Name,
                    BarberShopId = e.BarberShopId
                }).ToList()
            },
            Services = barbershopDetails.Services.Select(s => new BarbershopServiceDto
            {
                BarbershopServiceId = s.Id,
                Price = s.Price,
                Duration = s.Duration,
                DescriptionOverride = s.DescriptionOverride,
                BarbershopId = s.BarbershopId,
                ServiceId = s.ServiceId
            }).ToList(),
            Bookings = barbershopDetails.Bookings.Select(b => new ResponseBookingJson
            {
                Id = b.Id,
                Date = b.Date,
                StartTime = b.StartTime,
                EndTime = b.EndTime
            }).ToList()
        };
    }
}