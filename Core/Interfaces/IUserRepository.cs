using Core.Enities;

namespace Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<Event>?> GetUserEvents(Guid userId);
}