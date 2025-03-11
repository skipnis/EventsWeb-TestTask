using Core.Enities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext context)
    {
        _users = context.Set<User>();
    }

    public async Task AddAsync(User entity)
    {
        await _users.AddAsync(entity);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<List<User>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public void UpdateAsync(User entity)
    { 
        _users.Update(entity);
    }

    public void DeleteAsync(User user)
    {
        _users.Remove(user);
    }

    public async Task<IEnumerable<Event>?> GetUserEvents(Guid userId)
    {
        var events = await _users
            .Where(u => u.Id == userId) 
            .SelectMany(u => u.EventUsers)  
            .Select(eu => eu.Event)  
            .ToListAsync();
        return events;
    }

    public async Task<User> GetByName(string name)
    {
        return await _users.FindAsync(name);
    }
}