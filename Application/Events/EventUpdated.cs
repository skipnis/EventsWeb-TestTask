using Application.Interfaces;
using Core.Enities;

namespace Application.Events;

public class EventUpdated : IEvent
{
    public Guid EventId { get; set; }
    public IEnumerable<User> Users { get; set; }

    public EventUpdated(Guid eventId, IEnumerable<User> users)
    {
        EventId = eventId;
        Users = users;
    }
}