namespace EstiloMestre.Communication.DTOs;

public class EmployeeDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long BarberShopId { get; set; }
}