namespace EstiloMestre.Communication.Responses;

public class ResponseBookingJson
{
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}