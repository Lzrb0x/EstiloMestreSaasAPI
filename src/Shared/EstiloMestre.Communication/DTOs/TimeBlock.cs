namespace EstiloMestre.Communication.DTOs;

public record TimeBlock(
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsDayOff = false);