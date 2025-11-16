using System.Collections.Generic;
using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseBarbershopDetailsJson
{
    public long Id { get; set; }
    public string BarbershopName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public long OwnerId { get; set; }
    public ResponseEmployeesJson Employees { get; set; } = new ResponseEmployeesJson();
    public IList<BarbershopServiceDto> Services { get; set; } = new List<BarbershopServiceDto>();
    public IList<ResponseBookingJson> Bookings { get; set; } = new List<ResponseBookingJson>();
}