using Core.Enities;

namespace Core.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    public Task<Event> GetByName(string name);
}