using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseGlobalServicesList
{
    public IList<GlobalServiceDto> GlobalServices { get; set; }
}