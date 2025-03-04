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

    public Task<IQueryable<User>> GetAllAsQueryableAsync()
    {
        return Task.FromResult(_users.AsQueryable());
    }

    public async Task<User> GetById(Guid id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<List<User>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task UpdateAsync(User entity)
    { 
        _users.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _users.FindAsync(id);
        _users.Remove(entity);
    }

    public async Task<User> GetByName(string name)
    {
        return await _users.FindAsync(name);
    }
}