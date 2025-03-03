using Core.Enities;

namespace Core.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    public Task<IEnumerable<Event>> GetByName(string name);
}