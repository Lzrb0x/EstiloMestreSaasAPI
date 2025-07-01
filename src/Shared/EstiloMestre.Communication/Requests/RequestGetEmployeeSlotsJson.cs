namespace EstiloMestre.Communication.Requests;

public class RequestGetEmployeeSlotsJson
{
    public DateOnly Date { get; set; }
    public TimeSpan ServiceDuration { get; set; }
}