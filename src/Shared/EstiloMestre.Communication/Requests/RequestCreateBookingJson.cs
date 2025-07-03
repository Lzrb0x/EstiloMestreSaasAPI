namespace EstiloMestre.Communication.Requests;

public class RequestCreateBookingJson
{
    public long BarbershopId { get; set; }
    public long BarbershopServiceId { get; set; }
    public long EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
}