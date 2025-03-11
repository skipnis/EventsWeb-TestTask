using Core.Enities;

namespace Core.Interfaces;

public interface IEventUserRepository
{
    Task AddAsync(EventUser user, CancellationToken cancellationToken);
    Task<EventUser?> GetByEventAndUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken);
    Task RemoveAsync(Guid eventId, Guid userId);
}