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

    public async Task AddAsync(EventUser user)
    {
        await _eventUsers.AddAsync(user);
    }

    public async Task<EventUser?> GetByEventAndUserAsync(Guid eventId, Guid userId)
    {
       return await _eventUsers.FirstOrDefaultAsync(eu => eu.EventId == eventId && eu.UserId == userId);
    }

    public async Task RemoveAsync(Guid eventId, Guid userId)
    {
        var registration = await _eventUsers.FindAsync(eventId, userId);
        _eventUsers.Remove(registration);
    }
}