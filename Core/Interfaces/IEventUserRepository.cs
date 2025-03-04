using Core.Enities;

namespace Core.Interfaces;

public interface IEventUserRepository
{
    Task AddAsync(EventUser user);
    Task<EventUser?> GetByEventAndUserAsync(Guid eventId, Guid userId);
    Task RemoveAsync(Guid eventId, Guid userId);
}