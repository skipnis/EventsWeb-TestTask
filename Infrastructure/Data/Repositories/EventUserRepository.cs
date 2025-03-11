using Core.Enities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class EventUserRepository : IEventUserRepository
{
    private readonly DbSet<EventUser> _eventUsers;

    public EventUserRepository(ApplicationDbContext context)
    {
        _eventUsers = context.Set<EventUser>();
    }

    public async Task AddAsync(EventUser user, CancellationToken cancellationToken)
    {
        await _eventUsers.AddAsync(user, cancellationToken);
    }

    public async Task<EventUser?> GetByEventAndUserAsync(Guid eventId, Guid userId, CancellationToken cancellationToken)
    {
       return await _eventUsers.FirstOrDefaultAsync(eu => eu.EventId == eventId && eu.UserId == userId, cancellationToken);
    }

    public async Task RemoveAsync(Guid eventId, Guid userId)
    {
        var registration = await _eventUsers.FindAsync(eventId, userId);
        _eventUsers.Remove(registration);
    }
}