namespace EstiloMestre.Communication.Requests;

public class RequestWorkingHourOverrideJson
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsDayOff { get; set; }
}