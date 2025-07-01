using EstiloMestre.Communication.Enums;

namespace EstiloMestre.Communication.DTOs;

public record TimelineEvent(
    TimeOnly Time,
    EventType EventType
);