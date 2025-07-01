using EstiloMestre.Application.Services.GetEmployeeWorkingBlocks;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Enums;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Booking;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Employee.Slots;

public class GetEmployeeSlotsUseCase(
    GetEmployeeWorkingBlocks workingBlockHelper,
    IBookingRepository bookingRepository,
    IBarbershopServiceRepository barbershopServiceRepository
) : IGetEmployeeSlotsUseCase
{
    public async Task<ResponseEmployeeSlotsJson> Execute(long employeeId, DateOnly date, long barbershopServiceId)
    {
        ValidateRequest(date);

        var workingBlocks = await workingBlockHelper.GetWorkingBlocks(employeeId, date);

        if (!workingBlocks.Any() || workingBlocks.Any(x => x.IsDayOff))
        {
            return new ResponseEmployeeSlotsJson { Slots = [], IsDayOff = true };
        }

        var bookings = await bookingRepository.GetByEmployeeIdAndDate(employeeId, date);

        var timelineEvents = new List<TimelineEvent>();

        timelineEvents.AddRange(workingBlocks.SelectMany(block => new[]
        {
            new TimelineEvent(block.StartTime, EventType.WorkPeriodStart),
            new TimelineEvent(block.EndTime, EventType.WorkPeriodEnd)
        }));

        timelineEvents.AddRange(bookings.SelectMany(b => new[]
        {
            new TimelineEvent(b.StartTime, EventType.BookingStart),
            new TimelineEvent(b.EndTime, EventType.BookingEnd)
        }));

        var sortedTimeline = timelineEvents.OrderBy(x => x.Time).ThenBy(e => e.EventType).ToList();

        var service = await barbershopServiceRepository.GetById(barbershopServiceId);
        if (service == null)
            throw new BusinessRuleException(ResourceMessagesExceptions.SERVICE_WITH_ID_NOT_FOUND);

        var slots = CalculateAvailableSlots(sortedTimeline, service.Duration);

        return new ResponseEmployeeSlotsJson
        {
            Slots = slots,
            IsDayOff = false
        };
    }

    private static List<SlotDto> CalculateAvailableSlots(List<TimelineEvent> timeline,
        TimeSpan serviceDuration)
    {
        var availableSlots = new List<SlotDto>();

        const int slotStepMinutes = 15;
        var nestingLevel = 1;
        TimeOnly? windowStartTime = null;


        var processingTimeline =
            timeline.Concat([new TimelineEvent(TimeOnly.MaxValue, EventType.WorkPeriodEnd)]);

        foreach (var (windowEndTime, eventType) in processingTimeline)
        {
            if (windowStartTime.HasValue)
            {
                var windowDuration = windowEndTime - windowStartTime.Value;

                if (windowDuration >= serviceDuration)
                {
                    var potentialSlotTime = windowStartTime.Value;
                    while (potentialSlotTime.Add(serviceDuration) <= windowEndTime)
                    {
                        availableSlots.Add(new SlotDto
                        {
                            Time = potentialSlotTime
                        });
                        potentialSlotTime = potentialSlotTime.AddMinutes(slotStepMinutes);
                    }
                }
            }

            if (eventType is EventType.WorkPeriodEnd or EventType.BookingStart)
            {
                nestingLevel++;
            }
            else
            {
                nestingLevel--;
            }


            if (nestingLevel == 0)
            {
                windowStartTime = windowEndTime;
            }
            else
            {
                windowStartTime = null;
            }
        }

        return availableSlots.OrderBy(s => s.Time).ToList();
    }


    private static void ValidateRequest(DateOnly date)
    {
        if (date < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new BusinessRuleException(ResourceMessagesExceptions.DATE_IN_THE_PAST);
        }
    }
}