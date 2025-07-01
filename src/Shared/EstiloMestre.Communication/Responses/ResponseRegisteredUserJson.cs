namespace EstiloMestre.Communication.Responses;

public class ResponseRegisteredUserJson
{
    public string Name { get; set; } = string.Empty;
    public TokensDto Tokens { get; set; } = default!;
}