using Core.Enities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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

    public async Task<IEnumerable<Event>> GetAll()
    {
        return await _events.ToListAsync();
    }

    public async Task<Event> GetById(Guid id)
    {
        return await _events.FindAsync(id);
    }

    public async Task<IEnumerable<Event>> GetPaginated(int pageNumber, int pageSize)
    {
        return await _events.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task Update(Event entity)
    { 
        _events.Update(entity);
    }

    public async Task DeleteById(Guid id)
    {
        var entity = await _events.FindAsync(id);
        _events.Remove(entity);
    }

    public async Task<Event> GetByName(string name)
    {
        return await _events.FindAsync(name);
    }
}