using Application.Interfaces;
using Core.Enities;

namespace Application.Events;

public class EventUpdated : IEvent
{
    public Guid EventId { get; set; }

    public EventUpdated(Guid eventId)
    {
        EventId = eventId;
    }
}