using Core.Enities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _users.ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetPaginated(int pageNumber, int pageSize)
    {
        return await _users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task Update(User entity)
    { 
        _users.Update(entity);
    }

    public async Task DeleteById(Guid id)
    {
        var entity = await _users.FindAsync(id);
        _users.Remove(entity);
    }

    public async Task<User> GetByName(string name)
    {
        return await _users.FindAsync(name);
    }
}