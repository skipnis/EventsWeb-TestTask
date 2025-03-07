using Core.Enities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DbSet<Event> _events;

    public EventRepository(ApplicationDbContext context)
    {
        _events = context.Set<Event>();
    }

    public async Task AddAsync(Event entity)
    {  
        await _events.AddAsync(entity);
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _events.ToListAsync();
    }

    public Task<IQueryable<Event>> GetAllAsQueryableAsync()
    {
        return Task.FromResult(_events.AsQueryable());
    }

    public async Task<Event> GetById(Guid id)
    {
        return await _events.FindAsync(id);
    }

    public async Task<List<Event>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _events.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task UpdateAsync(Event entity)
    { 
        _events.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _events.FindAsync(id);
        _events.Remove(entity);
    }

    public async Task<Event> GetByName(string name)
    {
        return await _events.FindAsync(name);
    }

    public async Task<IEnumerable<User>?> GetParticipants(Guid eventId)
    {
        var participants = await _events
            .Where(e=>e.Id == eventId)
            .SelectMany(e=> e.EventUsers)
            .Select(e=>e.User)
            .ToListAsync();
        
        return participants;
    }

    public async Task AddImage(Guid eventId, string imageUrl)
    {
        var eventEntity = _events.Find(eventId);
        eventEntity.SetImageUrl(imageUrl);
        _events.Update(eventEntity);
    }
}