using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseEmployeeSlotsJson
{
    public IList<SlotDto> Slots { get; set; } = [];
    public bool IsDayOff { get; set; }
}