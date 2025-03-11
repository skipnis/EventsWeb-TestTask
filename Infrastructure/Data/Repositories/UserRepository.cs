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

    public async Task AddAsync(User entity, CancellationToken cancellationToken)
    {
        await _users.AddAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _users.ToListAsync(cancellationToken);
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _users.FindAsync(id, cancellationToken);
    }

    public async Task<List<User>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public void UpdateAsync(User entity)
    { 
        _users.Update(entity);
    }

    public void DeleteAsync(User user)
    {
        _users.Remove(user);
    }

    public async Task<IEnumerable<Event>?> GetUserEvents(Guid userId, CancellationToken cancellationToken)
    {
        var events = await _users
            .Where(u => u.Id == userId) 
            .SelectMany(u => u.EventUsers)  
            .Select(eu => eu.Event)  
            .ToListAsync(cancellationToken);
        return events;
    }

    public async Task<User> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _users.FindAsync(name, cancellationToken);
    }
}