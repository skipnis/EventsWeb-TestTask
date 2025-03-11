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

    public async Task<Event> GetById(Guid id)
    {
        return await _events.FindAsync(id);
    }

    public async Task<List<Event>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _events.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public void UpdateAsync(Event entity)
    { 
        _events.Update(entity);
    }

    public void DeleteAsync(Event eventEntity)
    {
        _events.Remove(eventEntity);
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

    public async Task<List<Event>> GetFilteredEvents(DateTime? dateFrom, DateTime? dateTo, List<string>? categories, List<string>? locations)
    {
        return await _events
            .Where(e => !dateFrom.HasValue || e.Date >= dateFrom.Value)
            .Where(e => !dateTo.HasValue || e.Date <= dateTo.Value)
            .Where(e => categories == null || !categories.Any() || categories.Contains(e.Category.Name))
            .Where(e => locations == null || !locations.Any() || locations.Contains(e.Place.Address))
            .ToListAsync();
    }
}